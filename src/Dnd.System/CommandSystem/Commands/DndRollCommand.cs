namespace Dnd.System.CommandSystem.Commands;

using Dnd.GameManagers.Dice;
using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities.Advantage;
using Dnd.System.Entities.GameActors;

public abstract class DndRollCommand : ICommand
{
    public DndRollCommand(IEventListener eventListener, IGameActor character, EAdvantage advantage, DiceRoll diceRoll)
    {
        EventListener = eventListener;
        Character = character;
        DiceRoll = diceRoll;
        Advantage = advantage;
        RollResult = RollResult.Empty();
    }

    public IEventListener EventListener { get; }

    public EAdvantage Advantage { get; }

    public DiceRoll DiceRoll { get; }

    public IGameActor Character { get; }

    public RollResult RollResult { get; }

    protected bool ShouldVisitEntities { get; set; }

    public bool IsForceCompleted { get; private set; }

    public RollResult Execute()
    {
        RollResult.Reset();

        RollResult.AddRoll(RollDice());

        if (Advantage.HasAdvantage() || Advantage.HasDisadvantage())
        {
            RollResult.AddAdvantageRoll(Advantage, RollDice());
        }

        InitializeResult();

        if (!IsForceCompleted && ShouldVisitEntities && RollResult.IsSuccess)
        {
            Character.HandleCommand(this);
        }

        if (!IsForceCompleted)
        {
            FinalizeResult();
        }

        return RollResult;
    }

    protected virtual void InitializeResult() { }

    protected virtual void FinalizeResult() { }

    public void ForceComplete()
    {
        IsForceCompleted = true;
    }

    protected int[] RollDice()
    {
        return DiceRoll.Roll();
    }

    ICommandResult ICommand.Execute()
    {
        return Execute();
    }

    public void AddAdvantageRoll(EAdvantage advantage, int[] roll)
    {
        if (!IsForceCompleted)
        {
            RollResult.AddAdvantageRoll(advantage, roll);
        }
    }

    public void SetRollAndReturn(IntegerResultWithBonus bonus, EAdvantage advantage, List<int[]> rolls)
    {
        if (!IsForceCompleted)
        {
            RollResult.Set(bonus, advantage, rolls);
            ForceComplete();
        }
    }

    public void SetErrorAndReturn(string errorMessage)
    {
        if (!IsForceCompleted)
        {
            RollResult.SetError(errorMessage);
            ForceComplete();
        }
    }
}
