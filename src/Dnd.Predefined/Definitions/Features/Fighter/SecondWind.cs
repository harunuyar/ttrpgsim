namespace Dnd.Predefined.Definitions.Features.Fighter;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.Feature;
using Dnd.Context;
using Dnd.Predefined.Commands.ListCommands;
using Dnd.Predefined.Definitions.Actions.Fighter;
using Dnd.Predefined.Instances;
using Dnd.System.CommandSystem.Commands;

public class SecondWind : FeatureInstance
{
    public static async Task<SecondWind> Create()
    {
        var featureModel = await DndContext.Instance.GetObject<FeatureModel>(Features.SecondWind);
        return featureModel == null 
            ? throw new InvalidOperationException("Second Wind feature model is not found") 
            : new SecondWind(featureModel);
    }

    private SecondWind(FeatureModel featureModel) : base(featureModel, [], [], [])
    {
        SecondWindAction = new SecondWindAction();
    }

    public SecondWindAction SecondWindAction { get; }

    public override async Task HandleCommand(ICommand command)
    {
        await base.HandleCommand(command);

        if (command is GetActions actions)
        {
            actions.AddValue(SecondWindAction, FeatureModel.Name ?? "Second Wind");
        }
    }
}
