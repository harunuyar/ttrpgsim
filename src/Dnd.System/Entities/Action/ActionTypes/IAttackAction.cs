namespace Dnd.System.Entities.Action.ActionTypes;

using Dnd._5eSRD.Models.Spell;

public interface IAttackAction : ITargetingAction
{
    EAttackType AttackType { get; }
}
