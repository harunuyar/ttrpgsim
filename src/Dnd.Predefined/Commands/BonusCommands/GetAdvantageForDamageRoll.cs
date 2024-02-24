namespace Dnd.Predefined.Commands.BonusCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class GetAdvantageForDamageRoll : ListCommand<EAdvantage>
{
    public GetAdvantageForDamageRoll(IGameActor actor, IAttackAction attackAction, IGameActor? target) : base(actor)
    {
        Target = target;
        AttackAction = attackAction;
    }

    public IGameActor? Target { get; }

    public IAttackAction AttackAction { get; }

    protected override async Task InitializeResult()
    {
        if (AttackAction is IWeaponAttackAction weaponAttackAction)
        {
            await weaponAttackAction.Weapon.HandleUsageCommand(this);
        }

        if (Target is not null)
        {
            var advantageFromTarget = await new GetAdvantageForDamageRollAgainst(Target, AttackAction).Execute();

            if (!advantageFromTarget.IsSuccess)
            {
                SetError("GetAdvantageForDamageRollAgainst: " + advantageFromTarget.ErrorMessage);
                return;
            }

            Add(advantageFromTarget);
        }
    }
}
