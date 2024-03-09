namespace Dnd.Predefined.Definitions.Features.Fighter;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.Feature;
using Dnd.Context;
using Dnd.Predefined.Commands.ListCommands;
using Dnd.Predefined.Definitions.Actions.Fighter;
using Dnd.Predefined.Instances;
using Dnd.System.CommandSystem.Commands;

public class FightingStyleProtection : FeatureInstance, ISubFightingStyle
{
    public static Task<FightingStyleProtection> Create()
    {
        return DndContext.Instance.GetObject<FeatureModel>(Features.FighterFightingStyleProtection).ContinueWith(
            t => t.Result == null
                ? throw new InvalidOperationException("FighterFightingStyleGreatWeaponFighting feature model is not found")
                : new FightingStyleProtection(t.Result));
    }

    private FightingStyleProtection(FeatureModel featureModel) : base(featureModel)
    {
        ProtectionAction = new ProtectionAction();
    }

    public ProtectionAction ProtectionAction { get; }

    public override async Task HandleCommand(ICommand command)
    {
        await base.HandleCommand(command);
        await ProtectionAction.HandleCommand(command);

        if (command is GetActions getActions)
        {
            getActions.AddValue(ProtectionAction, FeatureModel.Name ?? "Fighting Style: Protection");
        }
    }
}
