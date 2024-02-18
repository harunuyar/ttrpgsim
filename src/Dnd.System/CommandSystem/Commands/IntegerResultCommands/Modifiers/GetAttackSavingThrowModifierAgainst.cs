﻿namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands.Modifiers;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.Entities.Actions.BaseActions;
using Dnd.System.Entities.GameActors;

internal class GetAttackSavingThrowModifierAgainst : DndScoreCommand
{
    public GetAttackSavingThrowModifierAgainst(IGameActor character, IGameActor attacker, IAttackAction attackAction) : base(character)
    {
        Attacker = attacker;
        AttackAction = attackAction;
    }

    public IGameActor Attacker { get; }

    public IAttackAction AttackAction { get; }

    protected override void InitializeResult()
    {
        Result.SetBaseValue("Base", 0);
    }
}