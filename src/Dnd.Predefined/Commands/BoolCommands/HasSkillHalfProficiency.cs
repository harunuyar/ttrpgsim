namespace Dnd.Predefined.Commands.BoolCommands;

using Dnd._5eSRD.Models.Skill;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class HasSkillHalfProficiency : ValueCommand<bool>
{
    public HasSkillHalfProficiency(IGameActor character, SkillModel skill) : base(character)
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

        var hasProficiency = await new HasSkillProficiency(Actor, Skill).Execute();

        if (!hasProficiency.IsSuccess)
        {
            SetError("HasSkillProficiency: " + hasProficiency.ErrorMessage);
        }

        if (hasProficiency.Value)
        {
            SetValue(false, $"{Actor.Name} has {Skill.Name} skill proficiency.");
            ForceComplete();
        }

        SetValue(false, $"{Actor.Name} doesn't have {Skill} skill half proficiency.");
    }
}