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
        : base(name, eventOwner)
    {
        AmountAction = amountAction;
        Opponent = opponent;
        Critical = critical;
    }

    public IAmountAction AmountAction { get; }

    public bool Critical { get; }

    public IGameActor? Opponent { get; }

    public ListResult<DicePool>? Modifiers { get; private set; }

    public ListResult<EAdvantage>? Advantages { get; private set; }

    public ValueResult<int?>? PredeterminedAmountResult { get; private set; }

    public override async Task InitializeEvent()
    {
        PredeterminedAmountResult = await new GetPredeterminedAmount(EventOwner, AmountAction, Opponent).Execute();

        if (!PredeterminedAmountResult.IsSuccess)
        {
            throw new InvalidOperationException($"Failed to get predetermined amount result from {EventOwner.Name}");
        }

        var predAmountResult = PredeterminedAmountResult.Value;
        if (predAmountResult.HasValue)
        {
            AmountResult = predAmountResult.Value;
        }
        else
        {
            Modifiers = await new GetAmountModifiers(EventOwner, AmountAction, Opponent).Execute();

            if (!Modifiers.IsSuccess)
            {
                throw new InvalidOperationException($"Failed to get amount modifiers from {EventOwner.Name}");
            }

            Advantages = await new GetAmountAdvantage(EventOwner, AmountAction, Opponent).Execute();

            if (!Advantages.IsSuccess)
            {
                throw new InvalidOperationException($"Failed to get amount advantages from {EventOwner.Name}");
            }
        }

        SetEventPhase(EEventPhase.Initialized);
    }

    public override async Task<IEnumerable<IEvent>> RunEventImpl()
    {
        if (AmountResult is not null)
        {
            SetEventPhase(EEventPhase.DoneRunning);
            return [];
        }

        if (Modifiers is null || Advantages is null)
        {
            throw new InvalidOperationException("Amount modifiers or advantages are not initialized");
        }

        if (RawRollResults is null || ModifierRollResults is null)
        {
            var modifierDicePool = Modifiers.Values.Select(x => x.Item2).DefaultIfEmpty(new DicePool([], 0)).Aggregate((a, b) => a.Concat(b));
            var advantage = Advantages.Values.Select(x => x.Item2).DefaultIfEmpty(EAdvantage.None).Aggregate((a, b) => a | b);

            var dicePool = AmountAction.AmountDicePool;
            if (Critical)
            {
                dicePool = dicePool.Critical();
            }

            RawRollResults = dicePool.Rolls.SelectMany(r => Enumerable.Repeat(new DiceRollResult(r.DiceType, advantage, r.Negative), r.NumberOfDice));
            ModifierRollResults = modifierDicePool.Rolls.SelectMany(r => Enumerable.Repeat(new DiceRollResult(r.DiceType, EAdvantage.None, r.Negative), r.NumberOfDice));

            await CalculateRollResult();
        }

        SetEventPhase(EEventPhase.DoneRunning);
        return [];
    }

    public Task CalculateRollResult()
    {
        if (RawRollResults is null || ModifierRollResults is null || ModifierConstantResult is null)
        {
            throw new InvalidOperationException("Roll result is not initialized");
        }

        AmountResult = RawRollResults.Select(x => x.Result).DefaultIfEmpty(0).Sum()
                + ModifierRollResults.Select(x => x.Result).DefaultIfEmpty(0).Sum()
                + ModifierConstantResult
                + RawConstantResult;

        return Task.CompletedTask;
    }

    public IEvent CreateReRollEvent(IEnumerable<DiceRollResult> prevRolls, IEnumerable<DiceRollResult> prevModifierRolls)
    {
        var advantage = (Advantages?.Values ?? []).Select(x => x.Item2).DefaultIfEmpty(EAdvantage.None).Aggregate((a, b) => a | b);
        return new ReRollEvent(EventName + ": Reroll", EventOwner, prevRolls, prevModifierRolls, advantage);
    }

    public IEnumerable<DiceRollResult>? RawRollResults { get; private set; }

    public int? RawConstantResult => AmountAction.AmountDicePool.Bonus;

    public IEnumerable<DiceRollResult>? ModifierRollResults { get; private set; }

    public int? ModifierConstantResult => Modifiers?.Values?.Select(x => x.Item2.Bonus)?.DefaultIfEmpty(0)?.Sum();

    public int? AmountResult { get; set; }
}
