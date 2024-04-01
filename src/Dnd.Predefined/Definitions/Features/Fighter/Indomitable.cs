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

public class Indomitable : FeatureInstance
{
    public static async Task<Indomitable> Create()
    {
        var featureModel = await DndContext.Instance.GetObject<FeatureModel>(Features.Indomitable1Use);
        return featureModel == null 
            ? throw new InvalidOperationException("Indomitable1Use feature model is not found") 
            : new Indomitable(featureModel);
    }

    private Indomitable(FeatureModel featureModel) : base(featureModel)
    {
        IndomitableAction1 = new IndomitableAction(new ActionUsageLimit(EActionUsageLimitType.PerLongRest, 1));
        IndomitableAction2 = new IndomitableAction(new ActionUsageLimit(EActionUsageLimitType.PerLongRest, 2));
        IndomitableAction3 = new IndomitableAction(new ActionUsageLimit(EActionUsageLimitType.PerLongRest, 3));
    }

    public IndomitableAction IndomitableAction1 { get; }

    public IndomitableAction IndomitableAction2 { get; }

    public IndomitableAction IndomitableAction3 { get; }

    public override async Task HandleCommand(ICommand command)
    {
        await base.HandleCommand(command);
        await IndomitableAction1.HandleCommand(command);
        await IndomitableAction2.HandleCommand(command);
        await IndomitableAction3.HandleCommand(command);

        if (command is GetReactions actions)
        {
            var fighterClass = await DndContext.Instance.GetObject<ClassModel>(Classes.Fighter);

            if (fighterClass == null)
            {
                throw new InvalidOperationException($"{Classes.Fighter} class model is not found");
            }

            int fighterLevel = command.Actor.LevelInfo.GetLevelsInClass(fighterClass);

            if (fighterLevel >= 17)
            {
                actions.AddValue(IndomitableAction3, FeatureModel.Name ?? "Indomitable");
            }
            else if (fighterLevel >= 13)
            {
                actions.AddValue(IndomitableAction2, FeatureModel.Name ?? "Indomitable");
            }
            else if (fighterLevel >= 9)
            {
                actions.AddValue(IndomitableAction1, FeatureModel.Name ?? "Indomitable");
            }
        }
    }
}
