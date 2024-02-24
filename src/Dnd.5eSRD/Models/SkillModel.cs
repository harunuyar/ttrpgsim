namespace Dnd._5eSRD.Models.Skill;

using Dnd._5eSRD.Models.Common;

public class SkillModel : APIReference
{
    public APIReference? AbilityScore { get; set; }
    public List<string>? Desc { get; set; }
}