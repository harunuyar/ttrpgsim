namespace Dnd.Predefined.Commands.BonusCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.GameActor;

internal class GetAttackRollModifierAgainst : ListCommand<int>
{
    public GetAttackRollModifierAgainst(IGameActor character, IAttackRollAction attackAction) : base(character)
    {
        AttackAction = attackAction;
    }

    public IAttackRollAction AttackAction { get; }
}
