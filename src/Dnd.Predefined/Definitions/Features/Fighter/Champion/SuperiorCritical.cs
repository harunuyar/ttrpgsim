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

        if (command is GetRollActionResult rollResult)
        {
            if (rollResult.Action is IWeaponAttackAction && rollResult.RawDiceResult >= 18)
            {
                rollResult.AddValue(System.GameManagers.Dice.ERollResult.CriticalSuccess, FeatureModel.Name ?? "Superior Critical");
            }
        }
    }
}
