namespace Dnd.Predefined.Commands.BonusCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.GameActor;

public class GetDamageRollModifier : ListCommand<int>
{
    public GetDamageRollModifier(IGameActor character, IAttackAction attackAction, IGameActor? target) : base(character)
    {
        Target = target;
        AttackAction = attackAction;
    }

    public IGameActor? Target { get; }

    public IAttackAction AttackAction { get; }

    protected override async Task InitializeResult()
    {
        if (Target != null)
        {
            var damageModifierAgainst = await new GetDamageRollModifierAgainst(Target, Actor, AttackAction).Execute();

            if (!damageModifierAgainst.IsSuccess)
            {
                SetError("GetDamageRollModifierAgainst: " + damageModifierAgainst.ErrorMessage);
                return;
            }

            Add(damageModifierAgainst);
        }

        if (AttackAction?.Weapon != null)
        {
            await AttackAction.Weapon.HandleUsageCommand(this);
        }
    }
}
