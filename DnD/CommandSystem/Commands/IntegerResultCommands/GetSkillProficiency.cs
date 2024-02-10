namespace DnD.CommandSystem.Commands.IntegerResultCommands;

using DnD.Entities.Characters;
using DnD.Entities.Skills;

internal class GetSkillProficiency : DndScoreCommand
{
    public GetSkillProficiency(Character character, IDndSkill skill) : base(character)
    {
        Skill = skill;
    }

    public IDndSkill Skill { get; }

    public override void InitializeResult()
    {
        Result.SetBaseValue(Skill, Character.GetSkillProficiency(Skill));
    }
}