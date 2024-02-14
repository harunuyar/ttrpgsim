namespace Dnd.Predefined.Feats.Proficiency;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.CommandSystem.Commands.BooleanResultCommands;
using Dnd.System.CommandSystem.Commands.EventCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
using Dnd.System.Entities.Skills;

public class SkillProficiency : AFeat
{
    public SkillProficiency(ISkill skill, EProficiencyType proficiencyType) : base("Skill Proficiency", string.Empty)
    {
        Skill = skill;
        ProficiencyType = proficiencyType;
        IsOverridden = false;
    }

    public ISkill Skill { get; }

    public EProficiencyType ProficiencyType { get; private set; }

    private bool IsOverridden { get; set; }

    public override string Description => $"You are proficient at {Skill.Name} with {ProficiencyType}";

    public override void HandleCommand(ICommand command)
    {
        if (command is AddLevel addLevel)
        {
            if (IsOverridden)
            {
                return;
            }

            var newSkillProficiencies = addLevel.Level.Feats
                .Where(feat => feat is SkillProficiency skillProficiency && skillProficiency.Skill == Skill)
                .Select(feat => (SkillProficiency)feat);
            
            foreach (var newSkillProficiency in newSkillProficiencies)
            {
                if (newSkillProficiency.ProficiencyType >= ProficiencyType)
                {
                    IsOverridden = true;
                }
                else 
                {
                    newSkillProficiency.IsOverridden = true;
                }
            }
        }
        else if (command is HasSkillProficiency hasSkillProficiency)
        {
            if (hasSkillProficiency.Skill == Skill && ProficiencyType == EProficiencyType.FullProficiency)
            {
                hasSkillProficiency.SetValue(this, true);
            }
        }
        else if (command is HasSkillExpertise hasSkillExpertise)
        {
            if (hasSkillExpertise.Skill == Skill && ProficiencyType == EProficiencyType.Expertise)
            {
                hasSkillExpertise.SetValue(this, true);
            }
        }
        else if (command is HasSkillHalfProficiency hasSkillHalfProficiency)
        {
            if (hasSkillHalfProficiency.Skill == Skill && ProficiencyType == EProficiencyType.HalfProficiency)
            {
                hasSkillHalfProficiency.SetValue(this, true);
            }
        }
        else if (command is GetSkillModifier getSkillModifier && getSkillModifier.Skill == Skill)
        {
            if (IsOverridden)
            {
                return;
            }

            var getProficiencyBonus = new GetProficiencyBonus(getSkillModifier.Actor);
            var proficiencyBonusResult = getProficiencyBonus.Execute();

            if (proficiencyBonusResult.IsSuccess)
            {
                getSkillModifier.AddBonus(this, ProficiencyType.GetProficiencyModifier(proficiencyBonusResult.Value));
            }
            else
            {
                getSkillModifier.SetErrorAndReturn("Couldn't get proficiency bonus: " + proficiencyBonusResult.ErrorMessage);
            }
        }
    }
}
