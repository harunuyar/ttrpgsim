namespace Dnd.Predefined.Commands.BonusCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.GameActor;

internal class GetDamageRollModifierAgainst : ListCommand<int>
{
    public GetDamageRollModifierAgainst(IGameActor character, IGameActor attacker, IAttackAction attackAction) : base(character)
    {
        Attacker = attacker;
        AttackAction = attackAction;
    }

    public IGameActor Attacker { get; }

    public IAttackAction AttackAction { get; }
}
