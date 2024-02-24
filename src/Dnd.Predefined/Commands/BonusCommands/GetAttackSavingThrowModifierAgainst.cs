namespace Dnd.Predefined.Commands.BonusCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.GameActor;

internal class GetAttackSavingThrowModifierAgainst : ListCommand<int>
{
    public GetAttackSavingThrowModifierAgainst(IGameActor character, IGameActor target, ISavingThrowAttackAction attackAction) : base(character)
    {
        Target = target;
        AttackAction = attackAction;
    }

    public IGameActor Target { get; }

    public ISavingThrowAttackAction AttackAction { get; }

    protected override async Task InitializeResult()
    {
        if (AttackAction?.Weapon != null)
        {
            await AttackAction.Weapon.HandleUsageCommand(this);
        }
    }
}
