namespace Dnd.System.Entities.Action.ActionTypes;

using Dnd._5eSRD.Models.Skill;

public interface ISkillCheckAction : IRollAction
{
    SkillModel Skill { get; }
}
