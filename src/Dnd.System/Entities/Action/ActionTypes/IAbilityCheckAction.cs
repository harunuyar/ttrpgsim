namespace Dnd.System.Entities.Action.ActionTypes;

using Dnd._5eSRD.Models.AbilityScore;

public interface IAbilityCheckAction : IRollAction
{
    AbilityScoreModel Ability { get; }
}
