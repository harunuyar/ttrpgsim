namespace Dnd.Predefined.Commands.BoolCommands;

using Dnd._5eSRD.Models.Skill;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class HasSkillProficiency : ValueCommand<bool>
{
    public HasSkillProficiency(IGameActor character, SkillModel skill) : base(character)
    {
        Skill = skill;
    }

    public SkillModel Skill { get; }

    protected override async Task InitializeResult()
    {
        var hasExpertise = await new HasSkillExpertise(Actor, Skill).Execute();

        if (!hasExpertise.IsSuccess)
        {
            SetError("HasSkillExpertise: " + hasExpertise.ErrorMessage);
        }

        if (hasExpertise.Value)
        {
            SetValue(false, $"{Actor.Name} has {Skill.Name} skill expertise.");
            ForceComplete();
        }

        if (Skill.Url == null)
        {
            SetError($"{Skill.Name} skill url is null.");
        }

        var proficiency = await new HasProficiency(Actor, Skill).Execute();
        if (!proficiency.IsSuccess)
        {
            SetError("HasProficiency: " + proficiency.ErrorMessage);
            return;
        }

        Set(proficiency);
    }
}