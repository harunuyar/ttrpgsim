namespace Dnd.Predefined.Feats.Proficiency;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.CommandSystem.Commands.BooleanResultCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
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

    public override string Description => $"You are proficient at {Skill.Name} with {ProficiencyType} and will have proficiency bonus to your skill checks";

    public override void HandleCommand(ICommand command)
    {
        if (command is HasSkillProficiency hasSkillProficiency && hasSkillProficiency.Skill == Skill && ProficiencyType == EProficiencyType.FullProficiency)
        {
            hasSkillProficiency.SetValue(this, true);
        }
        else if (command is HasSkillExpertise hasSkillExpertise && hasSkillExpertise.Skill == Skill && ProficiencyType == EProficiencyType.Expertise)
        {
            hasSkillExpertise.SetValue(this, true);
        }
        else if (command is HasSkillHalfProficiency hasSkillHalfProficiency && hasSkillHalfProficiency.Skill == Skill && ProficiencyType == EProficiencyType.HalfProficiency)
        {
            hasSkillHalfProficiency.SetValue(this, true);
        }
        else if (command is GetSkillModifier getSkillModifier && getSkillModifier.Skill == Skill)
        {
            var getProficiencyBonus = new GetProficiencyBonus(getSkillModifier.Character);
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

    public void ChangeProficiencyLevel(EProficiencyType newProficiencyLevel)
    {
        ProficiencyType = newProficiencyLevel;
    }
}
