namespace Dnd.System.CommandSystem.Commands.BooleanResultCommands;

using Dnd.System.Entities.Characters;
using Dnd.System.Entities.Skills;

public class HasSkillHalfProficiency : DndBooleanCommand
{
    public HasSkillHalfProficiency(ICharacter character, ISkill skill) : base(character)
    {
        Skill = skill;
    }

    public ISkill Skill { get; }

    protected override void InitializeResult()
    {
        Result.SetValue("Default", false);
    }

    protected override void FinalizeResult()
    {
    }
}