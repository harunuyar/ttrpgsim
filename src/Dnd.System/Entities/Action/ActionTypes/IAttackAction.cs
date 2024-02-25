namespace Dnd.System.Entities.Action.ActionTypes;

using Dnd._5eSRD.Models.Spell;
using Dnd.System.GameManagers.Dice;

public enum ESuccessMeasuringType
{
    None,
    AttackRoll,
    SavingThrow,
    Guaranteed
}

public interface IAttackAction : IAction
{
    EAttackType AttackType { get; }
    ESuccessMeasuringType SuccessMeasuringType { get; }
    DicePool DamageDicePool { get; }
}
