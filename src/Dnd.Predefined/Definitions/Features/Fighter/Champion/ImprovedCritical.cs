namespace Dnd.Predefined.Definitions.Features.Fighter.Champion;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.Feature;
using Dnd.Context;
using Dnd.Predefined.Commands.RollBonusCommands;
using Dnd.Predefined.Instances;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action.ActionTypes;

public class ImprovedCritical : FeatureInstance, ISubFightingStyle
{
    public static Task<ImprovedCritical> Create()
    {
        return DndContext.Instance.GetObject<FeatureModel>(Features.ImprovedCritical).ContinueWith(
            t => t.Result == null 
                ? throw new InvalidOperationException("ImprovedCritical feature model is not found") 
                : new ImprovedCritical(t.Result));
    }

    private ImprovedCritical(FeatureModel featureModel) : base(featureModel)
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
