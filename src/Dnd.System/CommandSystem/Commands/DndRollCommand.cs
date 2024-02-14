namespace Dnd.System.CommandSystem.Commands;

using Dnd.GameManagers.Dice;
using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities.Advantage;
using Dnd.System.Entities.GameActors;

public abstract class DndRollCommand : ADndCommand<RollResult>
{
    public DndRollCommand(IEventListener eventListener, IGameActor character, EAdvantage advantage, DiceRoll diceRoll) : base(character)
    {
        EventListener = eventListener;
        DiceRoll = diceRoll;
        Advantage = advantage;

        Result = RollResult.Success(RollDice());
        if (Advantage.HasAdvantage() || Advantage.HasDisadvantage())
        {
            Result.AddAdvantageRoll(Advantage, RollDice());
        }
    }

    public IEventListener EventListener { get; }

    public EAdvantage Advantage { get; }

    public DiceRoll DiceRoll { get; }

    public override RollResult Result { get; }

    protected int[] RollDice()
    {
        return DiceRoll.Roll();
    }

    public void AddAdvantageRoll(EAdvantage advantage)
    {
        if (!IsForceCompleted)
        {
            Result.AddAdvantageRoll(advantage, RollDice());
        }
    }

    public void SetRollAndReturn(EAdvantage advantage, List<int[]> rolls)
    {
        if (!IsForceCompleted)
        {
            Result.Set(advantage, rolls);
            ForceComplete();
        }
    }
}
