namespace Dnd.Predefined.Definitions.Features.Fighter;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.Feature;
using Dnd.Context;
using Dnd.Predefined.Commands.ListCommands;
using Dnd.Predefined.Definitions.Actions.Fighter;
using Dnd.Predefined.Instances;
using Dnd.System.CommandSystem.Commands;

public class FightingStyleGreatWeaponFighting : FeatureInstance, ISubFightingStyle
{
    public static Task<FightingStyleGreatWeaponFighting> Create()
    {
        return DndContext.Instance.GetObject<FeatureModel>(Features.FighterFightingStyleGreatWeaponFighting).ContinueWith(
            t => t.Result == null
                ? throw new InvalidOperationException("FighterFightingStyleGreatWeaponFighting feature model is not found")
                : new FightingStyleGreatWeaponFighting(t.Result));
    }

    private FightingStyleGreatWeaponFighting(FeatureModel featureModel) : base(featureModel)
    {
        RerollDamageAction = new GreatWeaponFightingRerollDamageAction();
    }

    public GreatWeaponFightingRerollDamageAction RerollDamageAction { get; }

    public override async Task HandleCommand(ICommand command)
    {
        await base.HandleCommand(command);
        await RerollDamageAction.HandleCommand(command);

        if (command is GetActions actions)
        {
            actions.AddValue(RerollDamageAction, FeatureModel.Name ?? "Great Weapon Fighting");
        }
    }
}
