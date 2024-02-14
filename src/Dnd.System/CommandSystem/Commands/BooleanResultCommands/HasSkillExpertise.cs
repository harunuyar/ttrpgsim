namespace Dnd.System.CommandSystem.Commands.BooleanResultCommands;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Skills;

public class HasSkillExpertise : DndBooleanCommand
{
    public HasSkillExpertise(IGameActor character, ISkill skill) : base(character)
    {
        Skill = skill;
    }

    public ISkill Skill { get; }

    protected override void InitializeResult()
    {
        Result.SetValue("Default", false);
    }
}