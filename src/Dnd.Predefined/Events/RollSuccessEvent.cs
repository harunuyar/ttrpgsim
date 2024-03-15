namespace Dnd.Predefined.Events;

using Dnd.Predefined.Commands.RollBonusCommands;
using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class RollSuccessEvent : AEvent, ISuccessRollEvent
{
    public RollSuccessEvent(string name, IGameActor eventOwner, ISuccessRollAction action, int? targetResult, IGameActor? opponent) 
        : base(name, eventOwner)
    {
        SuccessRollAction = action;
        TargetResult = targetResult;
        Opponent = opponent;
    }

    public override bool IsWaitingForUserInput => TargetResult is null;

    public ISuccessRollAction SuccessRollAction { get; }

    public IGameActor? Opponent { get; }

    public int? TargetResult { get; }

    public ListResult<DicePool>? RollModifiers { get; private set; }

    public ListResult<EAdvantage>? RollAdvantages { get; private set; }

    public ListResult<ERollResult>? PredeterminedRollResults { get; private set; }

    public IRollEvent? RollEvent { get; private set; }

    private ERollResult? manualRollResult;

    public override async Task InitializeEvent()
    {
        PredeterminedRollResults = await new GetPredeterminedRollResult(EventOwner, SuccessRollAction, Opponent).Execute();

        if (!PredeterminedRollResults.IsSuccess)
        {
            throw new InvalidOperationException($"Failed to get predetermined roll results from {EventOwner.Name}");
        }

        var predRollResult = PredeterminedRollResults.Values.Select(x => x.Item2).DefaultIfEmpty(ERollResult.None).Aggregate((a, b) => a | b);

        if (predRollResult.IsMeaningful())
        {
            manualRollResult = predRollResult;
        }
        else
        {
            RollModifiers = await new GetModifiers(EventOwner, SuccessRollAction, Opponent).Execute();

            if (!RollModifiers.IsSuccess)
            {
                throw new InvalidOperationException($"Failed to get roll modifiers from {EventOwner.Name}");
            }

            RollAdvantages = await new GetAdvantage(EventOwner, SuccessRollAction, Opponent).Execute();

            if (!RollAdvantages.IsSuccess)
            {
                throw new InvalidOperationException($"Failed to get roll advantages from {EventOwner.Name}");
            }
        }
        
        SetEventPhase(EEventPhase.Initialized);
    }

    public override Task<IEnumerable<IEvent>> RunEventImpl()
    {
        if (RollModifiers is null || RollAdvantages is null)
        {
            SetEventPhase(EEventPhase.DoneRunning);
            return Task.FromResult<IEnumerable<IEvent>>([]);
        }

        var advantage = RollAdvantages.Values.Select(x => x.Item2).DefaultIfEmpty(EAdvantage.None).Aggregate((a, b) => a | b);
        var modifierDicePool = RollModifiers.Values.Select(x => x.Item2).DefaultIfEmpty(new DicePool([], 0)).Aggregate((a, b) => a.Concat(b));

        RollEvent = new RollEvent(EventName, EventOwner, new DicePool([new DiceRoll(1, EDiceType.d20)], 0), modifierDicePool, advantage);
        RollEvent.AddFinalAction(new Task(() => { SetEventPhase(EEventPhase.DoneRunning); }));

        SetEventPhase(EEventPhase.WaitingOtherEvent);
        return Task.FromResult<IEnumerable<IEvent>>([RollEvent]);
    }

    public override async Task<IEnumerable<IEvent>> FinalizeEvent()
    {
        if (RollResultModifiers is null && TotalResult.HasValue && RollResult.HasValue)
        {
            RollResultModifiers = await new GetRollActionResult(EventOwner, SuccessRollAction, Opponent, TotalResult.Value, RollResult.Value).Execute();

            if (!RollResultModifiers.IsSuccess)
            {
                throw new InvalidOperationException("Failed to get roll result modifiers. " + RollResultModifiers.ErrorMessage);
            }
        }
        
        return await base.FinalizeEvent();
    }

    public DiceRollResult? RawRollResult => RollEvent?.RollResults?.First();

    public IEnumerable<DiceRollResult>? ModifierRollResults => RollEvent?.RollResults?.Skip(1);

    public int? ConstantModifier => RollModifiers?.Values?.Select(x => x.Item2.Bonus)?.DefaultIfEmpty(0)?.Sum();

    public int? TotalResult => (RawRollResult is null || ModifierRollResults is null || ConstantModifier is null) 
        ? null
        : RawRollResult.Result + ModifierRollResults.Select(x => x.Result).DefaultIfEmpty(0).Sum() + ConstantModifier;

    public ERollResult? RollResult 
    { 
        set => manualRollResult = value; 
        get => manualRollResult ?? GetMergedRollResultModifier() ??
            ((RawRollResult is null || ModifierRollResults is null || ConstantModifier is null || TargetResult is null) ? null
                : RawRollResult.Result == 20 ? ERollResult.CriticalSuccess
                : RawRollResult.Result == 1 ? ERollResult.CriticalFailure
                : TotalResult >= TargetResult ? ERollResult.Success
                : ERollResult.Failure); }

    public ListResult<ERollResult>? RollResultModifiers { get; private set; }

    private ERollResult? GetMergedRollResultModifier()
    {
        if (RollResultModifiers is null || !RollResultModifiers.IsSuccess)
        {
            return null;
        }

        var result = RollResultModifiers.Values.Select(x => x.Item2).DefaultIfEmpty(ERollResult.None).Aggregate((a, b) => a | b);

        return result.IsMeaningful() ? result : null;
    }
}
