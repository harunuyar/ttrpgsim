namespace Dnd.Predefined.Events;

using Dnd.Predefined.Commands.RollBonusCommands;
using Dnd.Predefined.Commands.ScoreCommands;
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

    public ListResult<DicePool>? Modifiers { get; private set; }

    public ListResult<EAdvantage>? Advantages { get; private set; }

    public ListResult<ERollResult>? PredeterminedSuccessResults { get; private set; }

    public override async Task InitializeEvent()
    {
        PredeterminedSuccessResults = await new GetPredeterminedRollResult(EventOwner, SuccessRollAction, Opponent).Execute();

        if (!PredeterminedSuccessResults.IsSuccess)
        {
            throw new InvalidOperationException($"Failed to get predetermined roll results from {EventOwner.Name}");
        }

        var predRollResult = PredeterminedSuccessResults.Values.Select(x => x.Item2).DefaultIfEmpty(ERollResult.None).Aggregate((a, b) => a | b);

        if (predRollResult.IsMeaningful())
        {
            RollResult = predRollResult;
            await CalculateModifiedRollResult();
        }
        else
        {
            Modifiers = await new GetModifiers(EventOwner, SuccessRollAction, Opponent).Execute();

            if (!Modifiers.IsSuccess)
            {
                throw new InvalidOperationException($"Failed to get roll modifiers from {EventOwner.Name}");
            }

            Advantages = await new GetAdvantage(EventOwner, SuccessRollAction, Opponent).Execute();

            if (!Advantages.IsSuccess)
            {
                throw new InvalidOperationException($"Failed to get roll advantages from {EventOwner.Name}");
            }
        }
        
        SetEventPhase(EEventPhase.Initialized);
    }

    public override async Task<IEnumerable<IEvent>> RunEventImpl()
    {
        if (RollResult is not null)
        {
            SetEventPhase(EEventPhase.DoneRunning);
            return [];
        }

        if (Advantages is null || Modifiers is null)
        {
            throw new InvalidOperationException("Roll advantages or modifiers are not initialized");
        }

        if (RawRollResult is null || ModifierRollResults is null)
        {
            var advantage = Advantages.Values.Select(x => x.Item2).DefaultIfEmpty(EAdvantage.None).Aggregate((a, b) => a | b);
            var modifierDicePool = Modifiers.Values.Select(x => x.Item2).DefaultIfEmpty(new DicePool([], 0)).Aggregate((a, b) => a.Concat(b));

            RawRollResult = new DiceRollResult(EDiceType.d20, advantage);
            ModifierRollResults = modifierDicePool.Rolls.SelectMany(r => Enumerable.Repeat(new DiceRollResult(r.DiceType, EAdvantage.None, r.Negative), r.NumberOfDice));

            await CalculateRollResult();
        }

        SetEventPhase(EEventPhase.DoneRunning);
        return [];
    }

    public async Task CalculateRollResult()
    {
        if (RawRollResult is null || ModifierRollResults is null || ModifierConstantResult is null || TargetResult is null)
        {
            throw new InvalidOperationException("Roll result is not initialized");
        }

        var criticalSuccessThreshold = await new GetCriticalSuccessThreshold(EventOwner, SuccessRollAction).Execute();

        if (!criticalSuccessThreshold.IsSuccess)
        {
            throw new InvalidOperationException("Failed to get critical success threshold. " + criticalSuccessThreshold.ErrorMessage);
        }

        var criticalFailureThreshold = await new GetCriticalFailureThreshold(EventOwner, SuccessRollAction).Execute();

        if (!criticalFailureThreshold.IsSuccess)
        {
            throw new InvalidOperationException("Failed to get critical failure threshold. " + criticalFailureThreshold.ErrorMessage);
        }

        RollResult = RawRollResult.Result >= criticalSuccessThreshold.Value ? ERollResult.CriticalSuccess
            : RawRollResult.Result <= criticalFailureThreshold.Value ? ERollResult.CriticalFailure 
            : TotalResult >= TargetResult ? ERollResult.Success 
            : ERollResult.Failure;

        await CalculateModifiedRollResult();
    } 

    private async Task CalculateModifiedRollResult()
    {
        if (RollResult is null)
        {
            throw new InvalidOperationException("Roll result is not initialized");
        }

        PostDeterminedSuccessResults = await new GetPostDeterminedRollResult(EventOwner, SuccessRollAction, Opponent, TotalResult, RollResult.Value).Execute();

        if (!PostDeterminedSuccessResults.IsSuccess)
        {
            throw new InvalidOperationException("Failed to get roll result modifiers. " + PostDeterminedSuccessResults.ErrorMessage);
        }

        var modifiedRollResult = PostDeterminedSuccessResults.Values.Select(x => x.Item2).DefaultIfEmpty(ERollResult.None).Aggregate((a, b) => a | b);
        if (modifiedRollResult.IsMeaningful())
        {
            RollResult = modifiedRollResult;
        }
    }

    public IEvent CreateReRollEvent(IEnumerable<DiceRollResult> prevRolls, IEnumerable<DiceRollResult> prevModifierRolls)
    {
        var advantage = (Advantages?.Values ?? []).Select(x => x.Item2).DefaultIfEmpty(EAdvantage.None).Aggregate((a, b) => a | b);
        return new ReRollEvent(EventName + ": Reroll", EventOwner, prevRolls, prevModifierRolls, advantage);
    }

    public DiceRollResult? RawRollResult { get; private set; }

    public IEnumerable<DiceRollResult>? ModifierRollResults { get; private set; }

    public int? ModifierConstantResult => Modifiers?.Values?.Select(x => x.Item2.Bonus)?.DefaultIfEmpty(0)?.Sum();

    public int? TotalResult => (RawRollResult is null || ModifierRollResults is null || ModifierConstantResult is null) 
        ? null
        : RawRollResult.Result + ModifierRollResults.Select(x => x.Result).DefaultIfEmpty(0).Sum() + ModifierConstantResult;

    public ListResult<ERollResult>? PostDeterminedSuccessResults { get; private set; }

    public ERollResult? RollResult { get; private set; }
}
