﻿namespace Dnd.Predefined.Commands.BonusCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class GetPreDeterminedAttackRollResult : ListCommand<ERollResult>
{
    public GetPreDeterminedAttackRollResult(IGameActor actor, IAttackRollAction attackRollAction, IGameActor? target) : base(actor)
    {
        AttackRollAction = attackRollAction;
        Target = target;
    }

    public IAttackRollAction AttackRollAction { get; }

    public IGameActor? Target { get; }

    protected override async Task InitializeResult()
    {
        if (AttackRollAction is IWeaponAttackAction weaponAttackAction)
        {
            await weaponAttackAction.Weapon.HandleUsageCommand(this);
        }

        if (Target is not null)
        {
            var advantageFromTarget = await new GetPreDeterminedAttackRollResultAgainst(Target, AttackRollAction).Execute();

            if (!advantageFromTarget.IsSuccess)
            {
                SetError("GetPreDeterminedAttackRollResultAgainst: " + advantageFromTarget.ErrorMessage);
                return;
            }

            Add(advantageFromTarget);
        }
    }
}
