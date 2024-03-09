namespace Dnd.Predefined.Definitions.Features.Fighter;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.Feature;
using Dnd.Context;
using Dnd.Predefined.Commands.ScoreCommands;
using Dnd.Predefined.Instances;
using Dnd.System.CommandSystem.Commands;

public class FightingStyleDefense : FeatureInstance, ISubFightingStyle
{
    public static Task<FightingStyleDefense> Create()
    {
        return DndContext.Instance.GetObject<FeatureModel>(Features.FighterFightingStyleDefense).ContinueWith(
            t => t.Result == null 
                ? throw new InvalidOperationException("FighterFightingStyleDefense feature model is not found") 
                : new FightingStyleDefense(t.Result));
    }

    private FightingStyleDefense(FeatureModel featureModel) : base(featureModel)
    {
    }

    public override async Task HandleCommand(ICommand command)
    {
        await base.HandleCommand(command);

        if (command is GetArmorClass armorClass)
        {
            if (command.Actor.Inventory.Armor is not null)
            {
                armorClass.AddBonus(1, FeatureModel.Name ?? "Defense");
            }
        }
    }
}
