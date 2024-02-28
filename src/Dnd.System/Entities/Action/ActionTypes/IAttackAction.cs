namespace Dnd.System.Entities.Action.ActionTypes;

using Dnd._5eSRD.Models.Spell;

public interface IAttackAction : IDamageAction
{
    EAttackType AttackType { get; }
}
