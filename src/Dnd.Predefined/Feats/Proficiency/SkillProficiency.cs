namespace Dnd.Predefined.Feats.Proficiency;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.CommandSystem.Commands.BooleanResultCommands;
using Dnd.System.Entities.Skills;

public class SkillProficiency : AFeat
{
    public SkillProficiency(ISkill skill, EProficiencyType proficiencyType) : base("Skill Proficiency", string.Empty)
    {
        Skill = skill;
        ProficiencyType = proficiencyType;
    }

    public ISkill Skill { get; }

    public EProficiencyType ProficiencyType { get; private set; }

    public override string Description => $"You are proficient at {Skill.Name} with {ProficiencyType}";

    public override void HandleCommand(ICommand command)
    {
        if (command is HasSkillHalfProficiency hasSkillHalfProficiency)
        {
            if (hasSkillHalfProficiency.Skill == Skill && ProficiencyType == EProficiencyType.HalfProficiency)
            {
                var hasSkillProficiency = new HasSkillProficiency(hasSkillHalfProficiency.Actor, Skill).Execute();

                if (!hasSkillProficiency.IsSuccess)
                {
                    hasSkillHalfProficiency.SetErrorAndReturn("HasSkillProficiency: " + hasSkillProficiency.ErrorMessage);
                    return;
                }

                if (hasSkillProficiency.Value)
                {
                    hasSkillHalfProficiency.SetValueAndReturn(this, false, $"You already have proficiency on {Skill.Name}");
                    return;
                }

                var hasSkillExpertise = new HasSkillExpertise(hasSkillHalfProficiency.Actor, Skill).Execute();

                if (!hasSkillExpertise.IsSuccess)
                {
                    hasSkillHalfProficiency.SetErrorAndReturn("HasSkillExpertise: " + hasSkillExpertise.ErrorMessage);
                    return;
                }

                if (hasSkillExpertise.Value)
                {
                    hasSkillHalfProficiency.SetValue(this, false, $"You already have expertise on {Skill.Name}");
                    return;
                }

                hasSkillHalfProficiency.SetValue(this, true, $"You have half proficiency on {Skill.Name}");
            }
        }
        else if (command is HasSkillProficiency hasSkillProficiency)
        {
            if (hasSkillProficiency.Skill == Skill && ProficiencyType == EProficiencyType.FullProficiency)
            {
                var hasSkillExpertise = new HasSkillExpertise(hasSkillProficiency.Actor, Skill).Execute();

                if (!hasSkillExpertise.IsSuccess)
                {
                    hasSkillProficiency.SetErrorAndReturn("HasSkillExpertise: " + hasSkillExpertise.ErrorMessage);
                    return;
                }

                if (hasSkillExpertise.Value)
                {
                    hasSkillProficiency.SetValue(this, false, $"You already have expertise on {Skill.Name}");
                    return;
                }

                hasSkillProficiency.SetValue(this, true, $"You have proficiency on {Skill.Name}");
            }
        }
        else if (command is HasSkillExpertise hasSkillExpertise)
        {
            if (hasSkillExpertise.Skill == Skill && ProficiencyType == EProficiencyType.Expertise)
            {
                hasSkillExpertise.SetValue(this, true, $"You have expertise on {Skill.Name}");
            }
        }
    }
}
