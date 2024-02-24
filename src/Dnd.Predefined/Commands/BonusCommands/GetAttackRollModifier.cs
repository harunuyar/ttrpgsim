namespace Dnd.Predefined.Commands.BonusCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.GameActor;

public class GetAttackRollModifier : ListCommand<int>
{
    public GetAttackRollModifier(IGameActor character, IAttackRollAction attackAction, IGameActor? target) : base(character)
    {
        AttackAction = attackAction;
        Target = target;
    }

    public IAttackRollAction AttackAction { get; }

    public IGameActor? Target { get; }

    protected override async Task InitializeResult()
    {
        if (Target != null)
        {
            var attackModifierAgainst = await new GetAttackRollModifierAgainst(Target, AttackAction).Execute();

            if (!attackModifierAgainst.IsSuccess)
            {
                SetError("GetAttackModifierAgainst: " + attackModifierAgainst.ErrorMessage);
                return;
            }

            Add(attackModifierAgainst);
        }

        if (AttackAction is IWeaponAttackAction weaponAttackAction)
        {
            await weaponAttackAction.Weapon.HandleUsageCommand(this);
        }
    }
}
