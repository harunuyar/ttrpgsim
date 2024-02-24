namespace Dnd.Predefined.Commands.BonusCommands;

using Dnd._5eSRD.Models.AbilityScore;
using Dnd._5eSRD.Models.Skill;
using Dnd.Context;
using Dnd.Predefined.Commands.BoolCommands;
using Dnd.Predefined.Commands.ScoreCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class GetSkillModifier : ListCommand<int>
{
    public GetSkillModifier(IGameActor character, SkillModel skill) : base(character)
    {
        Skill = skill;
    }

    public SkillModel Skill { get; }

    protected override async Task InitializeResult()
    {
        if (Skill.AbilityScore?.Url == null)
        {
            SetError("Skill.AbilityScore?.Url is null");
            return;
        }

        var ability = await DndContext.Instance.GetObject<AbilityScoreModel>(Skill.AbilityScore.Url);
        if (ability == null)
        {
            SetError("AbilityScoreModel not found for " + Skill.AbilityScore.Url);
            return;
        }

        var attributeModifierResult = await new GetAbilityModifier(Actor, ability).Execute();

        if (!attributeModifierResult.IsSuccess)
        {
            SetError("GetAbilityModifier: " + attributeModifierResult.ErrorMessage);
            return;
        }

        AddValue(attributeModifierResult.Value, ability.FullName ?? string.Empty);

        var proficiencyBonus = await new GetProficiencyBonus(Actor).Execute();

        if (!proficiencyBonus.IsSuccess)
        {
            SetError("GetProficiencyBonus: " + proficiencyBonus.ErrorMessage);
            return;
        }

        var hasSkillExpertise = await new HasSkillExpertise(Actor, Skill).Execute();

        if (!hasSkillExpertise.IsSuccess)
        {
            SetError("HasSkillExpertise: " + hasSkillExpertise.ErrorMessage);
            return;
        }

        if (hasSkillExpertise.Value)
        {
            AddValue(proficiencyBonus.Value * 2, hasSkillExpertise.Message);
        }
        else
        {
            var hasSkillProficiency = await new HasSkillProficiency(Actor, Skill).Execute();

            if (!hasSkillProficiency.IsSuccess)
            {
                SetError("HasSkillProficiency: " + hasSkillProficiency.ErrorMessage);
                return;
            }

            if (hasSkillProficiency.Value)
            {
                AddValue(proficiencyBonus.Value, hasSkillProficiency.Message);
            }
            else
            {
                var hasSkillHalfProficiency = await new HasSkillHalfProficiency(Actor, Skill).Execute();

                if (!hasSkillHalfProficiency.IsSuccess)
                {
                    SetError("HasSkillHalfProficiency: " + hasSkillHalfProficiency.ErrorMessage);
                    return;
                }

                if (hasSkillHalfProficiency.Value)
                {
                    AddValue(proficiencyBonus.Value / 2, hasSkillHalfProficiency.Message);
                }
            }
        }
    }
}