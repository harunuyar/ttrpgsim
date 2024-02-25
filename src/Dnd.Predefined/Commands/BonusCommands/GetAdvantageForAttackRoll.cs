namespace Dnd.Predefined.Commands.BonusCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class GetAdvantageForAttackRoll : ListCommand<EAdvantage>
{
    public GetAdvantageForAttackRoll(IGameActor actor, IAttackRollAction attackRollAction, IGameActor? target) : base(actor)
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
            var advantageFromTarget = await new GetAdvantageForAttackRollAgainst(Target, AttackRollAction).Execute();

            if (!advantageFromTarget.IsSuccess)
            {
                SetError("GetAdvantageForAttackRollAgainst: " + advantageFromTarget.ErrorMessage);
                return;
            }

            Add(advantageFromTarget);
        }
    }
}
