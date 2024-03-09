namespace Dnd.Predefined.Definitions.Features.Common;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.AbilityScore;
using Dnd._5eSRD.Models.Feature;
using Dnd.Context;
using Dnd.Predefined.Commands.ScoreCommands;
using Dnd.Predefined.Instances;
using Dnd.System.CommandSystem.Commands;

public class AbilityScoreImprovement : FeatureInstance
{
    public static Task<AbilityScoreImprovement> Create(AbilityScoreModel abilityScore1, AbilityScoreModel abilityScore2)
    {
        return DndContext.Instance.GetObject<FeatureModel>(Features.FighterAbilityScoreImprovement1).ContinueWith(
            t => t.Result == null 
                ? throw new InvalidOperationException("FighterAbilityScoreImprovement1 feature model is not found") 
                : new AbilityScoreImprovement(t.Result, abilityScore1, abilityScore2));
    }

    private AbilityScoreImprovement(FeatureModel featureModel, AbilityScoreModel abilityScore1, AbilityScoreModel abilityScore2) : base(featureModel)
    {
        AbilityScore1 = abilityScore1;
        AbilityScore2 = abilityScore2;
    }

    AbilityScoreModel AbilityScore1 { get; }

    AbilityScoreModel AbilityScore2 { get; }

    public override async Task HandleCommand(ICommand command)
    {
        await base.HandleCommand(command);

        if (command is GetBaseAbilityScore baseAbilityScore)
        {
            if (baseAbilityScore.Ability == AbilityScore1 && baseAbilityScore.Ability == AbilityScore2)
            {
                baseAbilityScore.AddBonus(2, FeatureModel.Name ?? "Ability Score Improvement");
            }
            else if (baseAbilityScore.Ability == AbilityScore1 || baseAbilityScore.Ability == AbilityScore2)
            {
                baseAbilityScore.AddBonus(1, FeatureModel.Name ?? "Ability Score Improvement");
            }
        }
    }
}
