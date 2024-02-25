namespace Dnd.Predefined.Commands.BonusCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;

internal class GetAttackSavingThrowModifierAgainst : ListCommand<int>
{
    public GetAttackSavingThrowModifierAgainst(IGameActor character, IGameActor target, ISavingThrowAction attackAction) : base(character)
    {
        Target = target;
        AttackAction = attackAction;
    }

    public IGameActor Target { get; }

    public ISavingThrowAction AttackAction { get; }

    protected override async Task InitializeResult()
    {
        if (AttackAction is IWeaponAttackAction weaponAttackAction)
        {
            await weaponAttackAction.Weapon.HandleUsageCommand(this);
        }
    }
}
