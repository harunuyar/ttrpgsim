namespace Dnd.System.CommandSystem.Commands.RollCommands;

using Dnd.GameManagers.Dice;
using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.Entities.DiceModifiers;
using Dnd.System.Entities.GameActors;
using Dnd.System.Events.EventListener;

public class RollDamage : DndRollCommand
{
    public RollDamage(IEventListener eventListener, IGameActor character, EAdvantage advantage, DiceRoll diceRoll, bool critical) 
        : base(eventListener, character, advantage, critical ? diceRoll * 2 : diceRoll)
    {
        Critical = critical;
    }

    public bool Critical { get; }
}
