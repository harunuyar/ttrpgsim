namespace Dnd.System.CommandSystem.Commands.RollCommands;

using Dnd.GameManagers.Dice;
using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.Entities.DiceModifiers;
using Dnd.System.Entities.GameActors;
using Dnd.System.Events.EventListener;

public class RollConcentrationSavingThrow : DndRollCommand
{
    public RollConcentrationSavingThrow(IEventListener eventListener, IGameActor character, EAdvantage advantage) 
        : base(eventListener, character, advantage, new DiceRoll(1, EDiceType.d20))
    {
    }
}
