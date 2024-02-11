namespace Dnd.CommandSystem.Commands.BooleanResultCommands;

using Dnd.Entities.Characters;
using Dnd.Entities.Skills;

public class HasSkillProficiency : DndBooleanCommand
{
    public HasSkillProficiency(Character character, IDndSkill skill) : base(character)
    {
        Skill = skill;
    }

    public IDndSkill Skill { get; }

    public override void InitializeResult()
    {
        Result.SetValue("Base", Character.GetSkillProficiency(Skill));
    }
}