namespace Dnd.System.CommandSystem.Commands.BooleanResultCommands;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Skills;

public class HasSkillHalfProficiency : DndBooleanCommand
{
    public HasSkillHalfProficiency(IGameActor character, ISkill skill) : base(character)
    {
        Skill = skill;
    }

    public ISkill Skill { get; }

    protected override void InitializeResult()
    {
        Result.SetValue(false, $"{Actor.Name} doesn't have {Skill.Name} skill half proficiency.");
    }
}