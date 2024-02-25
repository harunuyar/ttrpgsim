﻿namespace Dnd.Predefined.Commands.BonusCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

internal class GetPreDeterminedSavingThrowResultAgainst : ListCommand<ERollResult>
{
    public GetPreDeterminedSavingThrowResultAgainst(IGameActor actor, ISavingThrowAction savingThrowAttackAction, IGameActor target) : base(actor)
    {
        SavingThrowAttackAction = savingThrowAttackAction;
        Target = target;
    }

    public ISavingThrowAction SavingThrowAttackAction { get; }

    public IGameActor Target { get; }

    protected override async Task InitializeResult()
    {
        if (SavingThrowAttackAction is IWeaponAttackAction weaponAttackAction)
        {
            await weaponAttackAction.Weapon.HandleUsageCommand(this);
        }
    }
}
