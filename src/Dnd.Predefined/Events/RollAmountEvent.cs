namespace Dnd.Predefined.Events;

using Dnd.Predefined.Commands.AmountBonusCommands;
using Dnd.Predefined.Commands.DamageBonusCommands;
using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class RollAmountEvent : AEvent, IAmountRollEvent
{
    public RollAmountEvent(string name, IGameActor eventOwner, IAmountAction amountAction, IGameActor? opponent, bool critical = false) 
        : base(name ,eventOwner)
    {
        AmountAction = amountAction;
        Opponent = opponent;
        Critical = critical;
    }

    public IAmountAction AmountAction { get; }

    public bool Critical { get; }

    public IGameActor? Opponent { get; }

    public ListResult<DicePool>? AmountModifiers { get; private set; }

    public ListResult<EAdvantage>? AmountAdvantages { get; private set; }

    public ValueResult<int?>? PredeterminedAmountResult { get; private set; }

    public IRollEvent? RollEvent { get; private set; }

    private int? manualAmountResult;

    public override async Task<IEnumerable<IEvent>> RunEventImpl()
    {
        if (PredeterminedAmountResult is null)
        {
            PredeterminedAmountResult = await new GetPredeterminedAmount(EventOwner, AmountAction, Opponent).Execute();

            if (!PredeterminedAmountResult.IsSuccess)
            {
                throw new InvalidOperationException($"Failed to get predetermined amount result from {EventOwner.Name}");
            }

            var predAmountResult = PredeterminedAmountResult.Value;
            if (predAmountResult.HasValue)
            {
                manualAmountResult = predAmountResult.Value;
            }
            else
            {
                AmountModifiers = await new GetAmountModifiers(EventOwner, AmountAction, Opponent).Execute();

                if (!AmountModifiers.IsSuccess)
                {
                    throw new InvalidOperationException($"Failed to get amount modifiers from {EventOwner.Name}");
                }

                AmountAdvantages = await new GetAmountAdvantage(EventOwner, AmountAction, Opponent).Execute();

                if (!AmountAdvantages.IsSuccess)
                {
                    throw new InvalidOperationException($"Failed to get amount advantages from {EventOwner.Name}");
                }
            }
        }

        if (manualAmountResult.HasValue)
        {
            SetEventPhase(EEventPhase.DoneRunning);
            return [];
        }
        
        if (AmountModifiers is null || AmountAdvantages is null)
        {
            throw new InvalidOperationException("Amount modifiers or advantages are not initialized");
        }

        var modifierDicePool = AmountModifiers.Values.Select(x => x.Item2).DefaultIfEmpty(new DicePool([], 0)).Aggregate((a, b) => a.Concat(b));

        if (modifierDicePool.Rolls.Count == 0 || AmountAction.AmountDicePool.Rolls.Count == 0)
        {
            SetEventPhase(EEventPhase.DoneRunning);
            return[];
        }

        var advantage = AmountAdvantages.Values.Select(x => x.Item2).DefaultIfEmpty(EAdvantage.None).Aggregate((a, b) => a | b);

        var dicePool = AmountAction.AmountDicePool;
        if (Critical)
        {
            dicePool = dicePool.Critical();
        }

        RollEvent = new RollEvent(EventName, EventOwner, dicePool, modifierDicePool, advantage);
        RollEvent.AddFinalAction(new Task(() => { SetEventPhase(EEventPhase.DoneRunning); }));

        SetEventPhase(EEventPhase.WaitingOtherEvent);
        return [RollEvent];
    }

    public IEnumerable<DiceRollResult>? RawRollResult => RollEvent?.RollResults;

    public int? RawConstantResult => AmountAction.AmountDicePool.Bonus;

    public IEnumerable<DiceRollResult>? ModifierRollResults => RollEvent?.ModifierRollResults;

    public int? ConstantModifier => AmountModifiers?.Values?.Select(x => x.Item2.Bonus)?.DefaultIfEmpty(0)?.Sum();

    public int? AmountResult => manualAmountResult ??
        (EventPhase != EEventPhase.Finalized 
            ? null
            : ((RawRollResult ?? []).Select(x => x.Result).DefaultIfEmpty(0).Sum() 
                + (ModifierRollResults ?? []).Select(x => x.Result).DefaultIfEmpty(0).Sum()
                + (ConstantModifier ?? 0) 
                + (RawConstantResult ?? 0)));
}
