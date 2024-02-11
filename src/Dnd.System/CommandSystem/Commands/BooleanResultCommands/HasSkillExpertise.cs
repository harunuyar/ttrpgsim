namespace Dnd.System.CommandSystem.Commands.BooleanResultCommands;

using Dnd.System.Entities.Characters;
using Dnd.System.Entities.Skills;

public class HasSkillExpertise : DndBooleanCommand
{
    public HasSkillExpertise(ICharacter character, ISkill skill) : base(character)
    {
        Skill = skill;
    }

    public ISkill Skill { get; }

    public override void InitializeResult()
    {
        Result.SetValue("Default", false);
    }
}