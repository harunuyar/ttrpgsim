namespace Dnd.System.CommandSystem.Commands;

using Dnd.GameManagers.Dice;
using Dnd.System.Entities.Characters;

public abstract class DndRollCommand : DndEventCommand
{
    public DndRollCommand(IEventListener eventListener, ICharacter character, EDiceType diceType) : base(eventListener, character)
    {
        DiceType = diceType;
    }

    public EDiceType DiceType { get; }

    protected int RollDice()
    {
        return DiceManager.RollDice(DiceType);
    }
}
