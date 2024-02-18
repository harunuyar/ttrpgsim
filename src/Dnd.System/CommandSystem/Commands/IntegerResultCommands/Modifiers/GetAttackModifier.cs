﻿namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands.Modifiers;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.Entities.Actions.BaseActions;
using Dnd.System.Entities.GameActors;

public class GetAttackModifier : DndScoreCommand
{
    public GetAttackModifier(IGameActor character, IGameActor? target, IAttackAction attackAction) : base(character)
    {
        Target = target;
        AttackAction = attackAction;
    }

    public IGameActor? Target { get; }

    public IAttackAction AttackAction { get; }

    protected override void InitializeResult()
    {
        Result.SetBaseValue("Base", 0);

        AttackAction.HandleCommand(this);

        if (Target != null)
        {
            var attackModifierAgainst = new GetAttackModifierAgainst(Target, Actor, AttackAction).Execute();

            if (!attackModifierAgainst.IsSuccess)
            {
                SetErrorAndReturn("GetAttackModifierAgainst: " + attackModifierAgainst.ErrorMessage);
                return;
            }

            Result.AddAsBonus("Attack Modifier From Target", attackModifierAgainst);
        }
    }
}