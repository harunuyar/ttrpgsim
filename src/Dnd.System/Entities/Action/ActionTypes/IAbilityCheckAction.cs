namespace Dnd.System.Entities.Action.ActionTypes;

using Dnd._5eSRD.Models.AbilityScore;

public interface IAbilityCheckAction : IAction
{
    AbilityScoreModel Ability { get; }
}
