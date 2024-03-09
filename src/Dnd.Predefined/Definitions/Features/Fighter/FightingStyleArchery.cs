namespace Dnd.Predefined.Definitions.Features.Fighter;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.Equipment;
using Dnd._5eSRD.Models.Feature;
using Dnd.Context;
using Dnd.Predefined.Commands.RollBonusCommands;
using Dnd.Predefined.Instances;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action.ActionTypes;

public class FightingStyleArchery : FeatureInstance, ISubFightingStyle
{
    public static Task<FightingStyleArchery> Create()
    {
        return DndContext.Instance.GetObject<FeatureModel>(Features.FighterFightingStyleArchery).ContinueWith(
            t => t.Result == null 
                ? throw new InvalidOperationException("FighterFightingStyleArchery feature model is not found") 
                : new FightingStyleArchery(t.Result));
    }

    private FightingStyleArchery(FeatureModel featureModel) : base(featureModel)
    {
    }

    public override async Task HandleCommand(ICommand command)
    {
        await base.HandleCommand(command);

        if (command is GetModifiers modifiers)
        {
            if (modifiers.Action is IWeaponAttackAction weaponAttack && weaponAttack.Weapon.EquipmentModel.WeaponRange == EWeaponRange.Ranged)
            {
                modifiers.AddValue(2, FeatureModel.Name ?? "Archery");
            }
        }
    }
}
