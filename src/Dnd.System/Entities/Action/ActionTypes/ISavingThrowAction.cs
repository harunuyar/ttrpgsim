namespace Dnd.System.Entities.Action.ActionTypes;

using Dnd._5eSRD.Models.AbilityScore;

public interface ISavingThrowAction : ISuccessRollAction, IDamageAction
{
    AbilityScoreModel Ability { get; }
    double SaveDamageMultiplier { get; }
}
