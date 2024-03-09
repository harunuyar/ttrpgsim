namespace Dnd.Predefined.Definitions.Features.Fighter;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.Class;
using Dnd._5eSRD.Models.Feature;
using Dnd.Context;
using Dnd.Predefined.Actions;
using Dnd.Predefined.Commands.ListCommands;
using Dnd.Predefined.Definitions.Actions.Fighter;
using Dnd.Predefined.Instances;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action;

public class ActionSurge : FeatureInstance
{
    public static async Task<ActionSurge> Create()
    {
        var featureModel = await DndContext.Instance.GetObject<FeatureModel>(Features.ActionSurge1Use);
        return featureModel == null 
            ? throw new InvalidOperationException("Action Surge feature model is not found") 
            : new ActionSurge(featureModel);
    }

    private ActionSurge(FeatureModel featureModel) : base(featureModel)
    {
        OneUseAction = new ActionSurgeAction(new ActionUsageLimit(EActionUsageLimitType.PerShortRest, 1));
        TwoUseAction = new ActionSurgeAction(new ActionUsageLimit(EActionUsageLimitType.PerShortRest, 2));
    }

    private readonly ActionSurgeAction OneUseAction, TwoUseAction;

    public override async Task HandleCommand(ICommand command)
    {
        await base.HandleCommand(command);
        await OneUseAction.HandleCommand(command);
        await TwoUseAction.HandleCommand(command);

        if (command is GetActions getActions)
        {
            var fighterClass = await DndContext.Instance.GetObject<ClassModel>(Classes.Fighter);

            if (fighterClass == null)
            {
                throw new InvalidOperationException($"{Classes.Fighter} class model is not found");
            }

            int fighterLevel = command.Actor.LevelInfo.GetLevelsInClass(fighterClass);

            if (fighterLevel >= 17)
            {
                getActions.AddValue(TwoUseAction, FeatureModel.Name ?? "Action Surge");
            }
            else if (fighterLevel >= 2)
            {
                getActions.AddValue(OneUseAction, FeatureModel.Name ?? "Action Surge");
            }
        }
    }
}
