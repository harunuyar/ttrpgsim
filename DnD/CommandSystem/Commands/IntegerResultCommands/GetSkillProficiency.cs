namespace DnD.CommandSystem.Commands.IntegerResultCommands;

using DnD.CommandSystem.Results;
using DnD.Entities.Characters;
using DnD.Entities.Skills;

internal class GetSkillProficiency : DndScoreCommand
{
    public GetSkillProficiency(Character character, IDndSkill skill) : base(character)
    {
        Skill = skill;
    }

    public IDndSkill Skill { get; }

    public override IntegerResultWithBonuses Execute()
    {
        return IntegerResultWithBonuses.Success(this, Skill.Name, Character.GetSkillProficiency(Skill), IntegerBonuses);
    }
}