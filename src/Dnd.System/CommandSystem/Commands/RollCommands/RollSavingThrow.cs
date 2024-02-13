﻿namespace Dnd.System.CommandSystem.Commands.RollCommands;

using Dnd.GameManagers.Dice;
using Dnd.System.Entities.Advantage;
using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.GameActors;

public class RollSavingThrow : DndRollCommand
{
    public RollSavingThrow(IEventListener eventListener, IGameActor character, EAttributeType attributeType, EAdvantage advantage) : base(eventListener, character, advantage, new DiceRoll(1, EDiceType.d20))
    {
        AttributeType = attributeType;
    }

    public EAttributeType AttributeType { get; }
}
