namespace DnD.CommandSystem.Commands.BooleanResultCommands;

using DnD.Entities.Characters;
using DnD.Entities.Skills;

internal class GetSkillProficiency : DndBooleanCommand
{
    public GetSkillProficiency(Character character, IDndSkill skill) : base(character)
    {
        Skill = skill;
    }

    public IDndSkill Skill { get; }

    public override void InitializeResult()
    {
        Result.SetValue("Base", Character.GetSkillProficiency(Skill));
    }
}