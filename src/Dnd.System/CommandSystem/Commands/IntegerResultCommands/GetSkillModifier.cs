namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.CommandSystem.Commands.BooleanResultCommands;
using Dnd.System.Entities;
using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Skills;

public class GetSkillModifier : DndScoreCommand
{
    public GetSkillModifier(IGameActor character, ISkill skill) : base(character)
    {
        Skill = skill;
    }

    public ISkill Skill { get; }

    protected override void InitializeResult()
    {
        Result.SetBaseValue("Base", 0);

        var attributeModifierResult = new GetAttributeModifier(Actor, Skill.AttributeType).Execute();

        if (!attributeModifierResult.IsSuccess)
        {
            SetErrorAndReturn("GetAttributeModifier: " + attributeModifierResult.ErrorMessage);
            return;
        }

        Result.AddAsBonus(Actor.AttributeSet.GetAttribute(Skill.AttributeType), attributeModifierResult);

        var proficiencyBonus = new GetProficiencyBonus(Actor).Execute();

        if (!proficiencyBonus.IsSuccess)
        {
            SetErrorAndReturn("GetProficiencyBonus: " + proficiencyBonus.ErrorMessage);
            return;
        }

        var hasSkillExpertise = new HasSkillExpertise(Actor, Skill).Execute();

        if (!hasSkillExpertise.IsSuccess)
        {
            SetErrorAndReturn("HasSkillExpertise: " + hasSkillExpertise.ErrorMessage);
            return;
        }

        if (hasSkillExpertise.Value)
        {
            Result.BonusCollection.AddBonus(hasSkillExpertise.Source ?? new CustomDndEntity("Skill Expertise"), proficiencyBonus.Value * 2);
        }
        else
        {
            var hasSkillProficiency = new HasSkillProficiency(Actor, Skill).Execute();

            if (!hasSkillProficiency.IsSuccess)
            {
                SetErrorAndReturn("HasSkillProficiency: " + hasSkillProficiency.ErrorMessage);
                return;
            }

            if (hasSkillProficiency.Value)
            {
                Result.BonusCollection.AddBonus(hasSkillProficiency.Source ?? new CustomDndEntity("Skill Proficiency"), proficiencyBonus.Value);
            }
            else
            {
                var hasSkillHalfProficiency = new HasSkillHalfProficiency(Actor, Skill).Execute();

                if (!hasSkillHalfProficiency.IsSuccess)
                {
                    SetErrorAndReturn("HasSkillHalfProficiency: " + hasSkillHalfProficiency.ErrorMessage);
                    return;
                }

                if (hasSkillHalfProficiency.Value)
                {
                    Result.BonusCollection.AddBonus(hasSkillHalfProficiency.Source ?? new CustomDndEntity("Skill Half Proficiency"), proficiencyBonus.Value / 2);
                }
            }
        }
    }
}