namespace Dnd.Predefined.Commands.BonusCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class GetPreDeterminedDamageRollResult : ListCommand<ERollResult>
{
    public GetPreDeterminedDamageRollResult(IGameActor actor, IAttackAction attackAction, IGameActor? target) : base(actor)
    {
        AttackAction = attackAction;
        Target = target;
    }

    public IAttackAction AttackAction { get; }

    public IGameActor? Target { get; }

    protected override async Task InitializeResult()
    {
        if (AttackAction is IWeaponAttackAction weaponAttackAction)
        {
            await weaponAttackAction.Weapon.HandleUsageCommand(this);
        }

        if (Target is not null)
        {
            var advantageFromTarget = await new GetPreDeterminedDamageRollResultAgainst(Target, AttackAction).Execute();

            if (!advantageFromTarget.IsSuccess)
            {
                SetError("GetAdvantageForDamageRollAgainst: " + advantageFromTarget.ErrorMessage);
                return;
            }

            Add(advantageFromTarget);
        }
    }
}
