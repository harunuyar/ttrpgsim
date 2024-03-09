namespace Dnd.Predefined.Definitions.Features.Fighter.Champion;

using Dnd._5eSRD.Models.Feature;
using Dnd.Context;
using Dnd.Predefined.Effects.Special;
using Dnd.Predefined.Instances;
using Dnd.System.CommandSystem.Commands;

public class Survivor : FeatureInstance
{
    public static async Task<Survivor> Create()
    {
        var featureModel = await DndContext.Instance.GetObject<FeatureModel>("Survivor");
        return featureModel == null ? throw new InvalidOperationException("Feature model for Survivor is not found") : new Survivor(featureModel);
    }

    private Survivor(FeatureModel featureModel) : base(featureModel)
    {
        SurvivorEffect = new SurvivorEffect(featureModel);
    }

    public SurvivorEffect SurvivorEffect { get; }

    public override async Task HandleCommand(ICommand command)
    {
        await base.HandleCommand(command);
        await SurvivorEffect.HandleCommand(command, command.Actor, command.Actor);
    }
}
