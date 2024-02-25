namespace Dnd.Predefined.Commands.BoolCommands;

using Dnd._5eSRD.Models.Skill;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class HasSkillExpertise : ValueCommand<bool>
{
    public HasSkillExpertise(IGameActor character, SkillModel skill) : base(character)
    {
        Skill = skill;
    }

    public SkillModel Skill { get; }

    protected override Task InitializeResult()
    {
        if (Skill.Url == null)
        {
            SetError($"{Skill.Name} skill url is null.");
            return Task.CompletedTask;
        }

        SetValue(false, $"{Actor.Name} doesn't have {Skill} skill expertise.");
        return Task.CompletedTask;
    }
}