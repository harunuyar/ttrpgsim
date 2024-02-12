namespace Dnd.Predefined.Feats;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.CommandSystem.Commands.BooleanResultCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
using Dnd.System.Entities.Skills;

public class SkillExpertise : AFeat
{
    public SkillExpertise(ISkill skill) : base("Skill Expertise", $"You are expert at {skill} and will have double proficiency bonus to your skill checks")
    {
        Skill = skill;
    }

    public ISkill Skill { get; }

    public override void HandleCommand(ICommand command)
    {
        if (command is HasSkillExpertise hasSkillExpertise && hasSkillExpertise.Skill == Skill)
        {
            hasSkillExpertise.Result.SetValue(this, true);
        }
        else if (command is GetSkillModifier getSkillModifier && getSkillModifier.Skill == Skill)
        {
            var getProficiencyBonus = new GetProficiencyBonus(getSkillModifier.Character);
            var proficiencyBonusResult = getProficiencyBonus.Execute();

            if (proficiencyBonusResult.IsSuccess)
            {
                // assumes that one proficiency bonus is already added by SkillProficiency
                getSkillModifier.Result.BonusCollection.AddBonus(this, proficiencyBonusResult.Value);
            }
            else
            {
                getSkillModifier.Result.SetError(proficiencyBonusResult.ErrorMessage ?? "Couldn't get proficiency bonus");
            }
        }
    }
}
