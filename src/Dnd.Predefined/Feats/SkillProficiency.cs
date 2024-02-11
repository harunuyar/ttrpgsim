namespace Dnd.Predefined.Feats;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.CommandSystem.Commands.BooleanResultCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
using Dnd.System.Entities.Skills;

public class SkillProficiency : AFeat
{
    public SkillProficiency(ISkill skill) : base("Skill Proficiency", $"You are proficient at {skill} and will have proficiency bonus to your skill checks")
    {
        Skill = skill;
    }

    public ISkill Skill { get; }

    public override void HandleCommand(DndCommand command)
    {
        if (command is HasSkillProficiency hasSkillProficiency && hasSkillProficiency.Skill == Skill)
        {
            hasSkillProficiency.Result.SetValue(this, true);
        }
        else if (command is GetSkillModifier getSkillModifier && getSkillModifier.Skill == Skill)
        {
            var getProficiencyBonus = new GetProficiencyBonus(getSkillModifier.Character);
            var proficiencyBonusResult = getProficiencyBonus.Execute();

            if (proficiencyBonusResult.IsSuccess)
            {
                getSkillModifier.Result.BonusCollection.AddBonus(this, proficiencyBonusResult.Value);
            }
            else
            {
                getSkillModifier.Result.SetError(proficiencyBonusResult.ErrorMessage ?? "Couldn't get proficiency bonus");
            }
        }
    }
}
