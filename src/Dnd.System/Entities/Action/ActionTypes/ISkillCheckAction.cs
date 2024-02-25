namespace Dnd.System.Entities.Action.ActionTypes;

using Dnd._5eSRD.Models.Skill;

public interface ISkillCheckAction : IAction, IAbilityCheckAction
{
    SkillModel Skill { get; }
}
