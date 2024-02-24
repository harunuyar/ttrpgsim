namespace Dnd.System.Entities.Action;

using Dnd._5eSRD.Models.AbilityScore;
using Dnd._5eSRD.Models.DamageType;

public interface ISavingThrowAction : IAction
{
    AbilityScoreModel Ability { get; }
    DamageTypeModel DamageType { get; }
}
