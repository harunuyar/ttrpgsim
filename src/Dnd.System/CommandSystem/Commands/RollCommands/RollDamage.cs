namespace Dnd.System.CommandSystem.Commands.RollCommands;

using Dnd.GameManagers.Dice;
using Dnd.System.Entities.Advantage;
using Dnd.System.Entities.GameActors;

public class RollDamage : DndRollCommand
{
    public RollDamage(IEventListener eventListener, IGameActor character, EAdvantage advantage, DiceRoll diceRoll) : base(eventListener, character, advantage, diceRoll)
    {
    }
}
