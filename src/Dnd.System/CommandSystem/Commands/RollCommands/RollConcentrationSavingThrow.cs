namespace Dnd.System.CommandSystem.Commands.RollCommands;

using Dnd.GameManagers.Dice;
using Dnd.System.Entities.Advantage;
using Dnd.System.Entities.GameActors;

public class RollConcentrationSavingThrow : DndRollCommand
{
    public RollConcentrationSavingThrow(IEventListener eventListener, IGameActor character, EAdvantage advantage) : base(eventListener, character, advantage, new DiceRoll(1, EDiceType.d20))
    {
    }
}
