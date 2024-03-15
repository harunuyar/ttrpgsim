namespace Dnd.Predefined.Definitions.Features.Fighter.Champion;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.Feature;
using Dnd.Context;
using Dnd.Predefined.Commands.RollBonusCommands;
using Dnd.Predefined.Instances;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action.ActionTypes;

public class SuperiorCritical : FeatureInstance, ISubFightingStyle
{
    public static Task<SuperiorCritical> Create()
    {
        return DndContext.Instance.GetObject<FeatureModel>(Features.SuperiorCritical).ContinueWith(
            t => t.Result == null 
                ? throw new InvalidOperationException("SuperiorCritical feature model is not found") 
                : new SuperiorCritical(t.Result));
    }

    private SuperiorCritical(FeatureModel featureModel) : base(featureModel)
    {
    }

    public override async Task HandleCommand(ICommand command)
    {
        await base.HandleCommand(command);

        if (command is GetCriticalSuccessThreshold criticalSuccessThreshold)
        {
            if (criticalSuccessThreshold.Action is IWeaponAttackAction)
            {
                criticalSuccessThreshold.AddBonus(-1, FeatureModel.Name ?? "Improved Critical");
            }
        }
    }
}
