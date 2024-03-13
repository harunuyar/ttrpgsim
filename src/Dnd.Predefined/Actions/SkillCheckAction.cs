namespace Dnd.Predefined.Actions;

using Dnd._5eSRD.Models.AbilityScore;
using Dnd._5eSRD.Models.Skill;
using Dnd.Context;
using Dnd.Predefined.Commands.BoolCommands;
using Dnd.Predefined.Commands.RollBonusCommands;
using Dnd.Predefined.Commands.ScoreCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;

public class SkillCheckAction : SuccessRollAction, ISkillCheckAction
{
    public SkillCheckAction(SkillModel skill)
        : base($"{skill.Name} Check", ActionDurationType.Action, ESuccessRollType.Skill, [])
    {
        Skill = skill;
    }

    public SkillModel Skill { get; }

    public override async Task HandleUsageCommand(ICommand command)
    {
        await base.HandleUsageCommand(command);

        if (command is GetModifiers modifiers)
        {
            if (Skill.AbilityScore?.Url == null)
            {
                modifiers.SetError("Skill.AbilityScore?.Url is null");
                return;
            }

            var ability = await DndContext.Instance.GetObject<AbilityScoreModel>(Skill.AbilityScore.Url);
            if (ability == null)
            {
                modifiers.SetError("AbilityScoreModel not found for " + Skill.AbilityScore.Url);
                return;
            }

            var attributeModifierResult = await new GetAbilityModifier(modifiers.Actor, ability).Execute();

            if (!attributeModifierResult.IsSuccess)
            {
                modifiers.SetError("GetAbilityModifier: " + attributeModifierResult.ErrorMessage);
                return;
            }

            modifiers.AddValue(attributeModifierResult.Value, ability.FullName ?? string.Empty);

            var proficiencyBonus = await new GetProficiencyBonus(modifiers.Actor).Execute();

            if (!proficiencyBonus.IsSuccess)
            {
                modifiers.SetError("GetProficiencyBonus: " + proficiencyBonus.ErrorMessage);
                return;
            }

            var hasSkillExpertise = await new HasSkillExpertise(modifiers.Actor, Skill).Execute();

            if (!hasSkillExpertise.IsSuccess)
            {
                modifiers.SetError("HasSkillExpertise: " + hasSkillExpertise.ErrorMessage);
                return;
            }

            if (hasSkillExpertise.Value)
            {
                modifiers.AddValue(proficiencyBonus.Value * 2, hasSkillExpertise.Message);
            }
            else
            {
                var hasSkillProficiency = await new HasSkillProficiency(modifiers.Actor, Skill).Execute();

                if (!hasSkillProficiency.IsSuccess)
                {
                    modifiers.SetError("HasSkillProficiency: " + hasSkillProficiency.ErrorMessage);
                    return;
                }

                if (hasSkillProficiency.Value)
                {
                    modifiers.AddValue(proficiencyBonus.Value, hasSkillProficiency.Message);
                }
                else
                {
                    var hasSkillHalfProficiency = await new HasSkillHalfProficiency(modifiers.Actor, Skill).Execute();

                    if (!hasSkillHalfProficiency.IsSuccess)
                    {
                        modifiers.SetError("HasSkillHalfProficiency: " + hasSkillHalfProficiency.ErrorMessage);
                        return;
                    }

                    if (hasSkillHalfProficiency.Value)
                    {
                        modifiers.AddValue(proficiencyBonus.Value / 2, hasSkillHalfProficiency.Message);
                    }
                }
            }
        }
    }
}
