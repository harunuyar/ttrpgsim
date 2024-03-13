namespace Dnd.System.Entities.Action.ActionTypes;

using Dnd._5eSRD.Models.Skill;

public interface ISkillCheckAction : ISuccessRollAction
{
    SkillModel Skill { get; }
}
