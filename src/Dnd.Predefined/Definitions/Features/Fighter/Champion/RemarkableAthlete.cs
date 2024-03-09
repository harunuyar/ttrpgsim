namespace Dnd.Predefined.Definitions.Features.Fighter.Champion;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.Feature;
using Dnd.Context;
using Dnd.Predefined.Commands.BoolCommands;
using Dnd.Predefined.Commands.RollBonusCommands;
using Dnd.Predefined.Commands.ScoreCommands;
using Dnd.Predefined.Instances;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action.ActionTypes;

public class RemarkableAthlete : FeatureInstance
{
    public static async Task<RemarkableAthlete> Create()
    {
        var featureModel = await DndContext.Instance.GetObject<FeatureModel>(Features.RemarkableAthlete);
        return new RemarkableAthlete(featureModel ?? throw new InvalidOperationException($"Feature model for {Features.RemarkableAthlete} is not found"));
    }

    private static readonly string[] Abilities = [AbilityScores.Str, AbilityScores.Dex, AbilityScores.Con];

    private RemarkableAthlete(FeatureModel featureModel) : base(featureModel)
    {
    }

    public override async Task HandleCommand(ICommand command)
    {
        await base.HandleCommand(command);

        if (command is GetModifiers modifiers)
        {
            if (modifiers.Action is ISkillCheckAction skillCheckAction && Abilities.Contains(skillCheckAction.Skill.AbilityScore?.Url))
            {
                var hasExpertise = await new HasSkillExpertise(modifiers.Actor, skillCheckAction.Skill).Execute();

                if (!hasExpertise.IsSuccess)
                {
                    modifiers.SetError("HasSkillExpertise: " + hasExpertise.ErrorMessage);
                    return;
                }

                if (hasExpertise.Value)
                {
                    return;
                }

                var hasProficiency = await new HasSkillProficiency(modifiers.Actor, skillCheckAction.Skill).Execute();

                if (!hasProficiency.IsSuccess)
                {
                    modifiers.SetError("HasSkillProficiency: " + hasProficiency.ErrorMessage);
                    return;
                }

                if (hasProficiency.Value)
                {
                    return;
                }

                var hasHalfProficiency = await new HasSkillHalfProficiency(modifiers.Actor, skillCheckAction.Skill).Execute();

                if (!hasHalfProficiency.IsSuccess)
                {
                    modifiers.SetError("HasSkillHalfProficiency: " + hasHalfProficiency.ErrorMessage);
                    return;
                }

                if (hasHalfProficiency.Value)
                {
                    return;
                }

                var bonus = await new GetProficiencyBonus(modifiers.Actor).Execute();

                if (!bonus.IsSuccess)
                {
                    modifiers.SetError("GetProficiencyBonus: " + bonus.ErrorMessage);
                    return;
                }

                modifiers.AddValue((int)Math.Ceiling(bonus.Value / 2.0f), FeatureModel.Name ?? "Remarkable Athlete");
            }
        }
    }
}
