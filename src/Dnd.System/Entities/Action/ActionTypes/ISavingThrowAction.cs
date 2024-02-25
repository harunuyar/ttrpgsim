namespace Dnd.System.Entities.Action.ActionTypes;

using Dnd._5eSRD.Models.AbilityScore;
using Dnd._5eSRD.Models.DamageType;

public interface ISavingThrowAction : IAttackAction
{
    AbilityScoreModel Ability { get; }
    DamageTypeModel DamageType { get; }
}
