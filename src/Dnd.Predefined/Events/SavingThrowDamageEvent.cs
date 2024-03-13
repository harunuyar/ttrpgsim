namespace Dnd.Predefined.Events;

using Dnd.Predefined.Commands.AmountBonusCommands;
using Dnd.Predefined.Commands.DamageBonusCommands;
using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class SavingThrowDamageEvent : DamageEvent, ISavingThrowDamageEvent
{
    public SavingThrowDamageEvent(string name, IGameActor eventOwner, ISavingThrowAttackAction damageAction, int amount, bool saved, IGameActor? attacker) 
        : base(name, eventOwner, damageAction, amount)
    {
        Saved = saved;
        Attacker = attacker;
    }

    public bool Saved { get; }

    public ISavingThrowAttackAction SavingThrowAttackAction { get => (ISavingThrowAttackAction)DamageAction; }

    public IGameActor? Attacker { get; }

    public ListResult<DicePool>? ModifiersFromTarget { get; private set; }

    public ValueResult<int?>? PredeterminedDamageFromTarget { get; private set; }

    public override async Task InitializeEvent()
    {
        ModifiersFromTarget = await new GetAmountModifiersFromOpponent(EventOwner, SavingThrowAttackAction, Attacker).Execute();
        
        if (!ModifiersFromTarget.IsSuccess)
        {
            throw new InvalidOperationException($"Failed to get modifiers from {EventOwner.Name}. " + ModifiersFromTarget.ErrorMessage);
        }

        PredeterminedDamageFromTarget = await new GetPredeterminedAmountFromOpponent(EventOwner, SavingThrowAttackAction, Attacker).Execute();

        if (!PredeterminedDamageFromTarget.IsSuccess)
        {
            throw new InvalidOperationException($"Failed to get predetermined damage from {EventOwner.Name}. " + PredeterminedDamageFromTarget.ErrorMessage);
        }

        SetEventPhase(EEventPhase.Initialized);
    }

    public override Task<IEnumerable<IEvent>> RunEvent()
    {
        if (ModifiersFromTarget is null || PredeterminedDamageFromTarget is null)
        {
            throw new InvalidOperationException("Event is not initialized");
        }

        if (EventPhase == EEventPhase.WaitingOtherEvent)
        {
            throw new InvalidOperationException("Waiting for other events to finish before continuing on this event");
        }

        if (PredeterminedDamageFromTarget.Value.HasValue)
        {
            Amount = PredeterminedDamageFromTarget.Value.Value;

            EventOwner.HitPoints.Damage(Amount);

            SetEventPhase(EEventPhase.DoneRunning);
            return Task.FromResult(Enumerable.Empty<IEvent>());
        }
        else if (ModifiersFromTarget.Values.Any(x => x.Item2.Rolls.Count != 0))
        {
            var dicePool = ModifiersFromTarget.Values.Select(x => x.Item2).DefaultIfEmpty(new DicePool([], 0)).Aggregate((a, b) => a.Concat(b));
            ModifierRollEvent = new RollEvent(EventName, EventOwner, new DicePool([], 0), dicePool, EAdvantage.None);
            ModifierRollEvent.AddFinalAction(new Task(() => SetEventPhase(EEventPhase.Initialized)));

            SetEventPhase(EEventPhase.WaitingOtherEvent);
            return Task.FromResult<IEnumerable<IEvent>>([ModifierRollEvent]);
        }

        Amount += ModifierRollEvent?.ModifierRollResults?.Select(x => x.Result).DefaultIfEmpty(0).Sum() ?? 0;
        Amount += ModifiersFromTarget?.Values.Select(x => x.Item2.Bonus).DefaultIfEmpty(0).Sum() ?? 0;

        EventOwner.HitPoints.Damage(Amount);

        SetEventPhase(EEventPhase.DoneRunning);
        return Task.FromResult(Enumerable.Empty<IEvent>());
    }

    public RollEvent? ModifierRollEvent { get; private set; }

    public void SetAmount(int amount)
    {
        Amount = amount;
    }
}
