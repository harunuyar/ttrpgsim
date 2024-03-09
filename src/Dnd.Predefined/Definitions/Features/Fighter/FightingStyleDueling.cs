namespace Dnd.Predefined.Definitions.Features.Fighter;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.Feature;
using Dnd.Context;
using Dnd.Predefined.Commands.DamageBonusCommands;
using Dnd.Predefined.Instances;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action.ActionTypes;

public class FightingStyleDueling : FeatureInstance, ISubFightingStyle
{
    public static Task<FightingStyleDueling> Create()
    {
        return DndContext.Instance.GetObject<FeatureModel>(Features.FighterFightingStyleDueling).ContinueWith(
            t => t.Result == null 
                ? throw new InvalidOperationException("FighterFightingStyleDueling feature model is not found") 
                : new FightingStyleDueling(t.Result));
    }

    private FightingStyleDueling(FeatureModel featureModel) : base(featureModel)
    {
    }

    public override async Task HandleCommand(ICommand command)
    {
        await base.HandleCommand(command);

        if (command is GetAmountModifiers modifiers)
        {
            if (modifiers.AmountAction is IWeaponAttackAction weaponAttack 
                && ((weaponAttack.Weapon == command.Actor.Inventory.MainHandWeapon && command.Actor.Inventory.OffHandWeapon is null) 
                    || (weaponAttack.Weapon == command.Actor.Inventory.OffHandWeapon && command.Actor.Inventory.MainHandWeapon is null)))
            {
                modifiers.AddValue(2, FeatureModel.Name ?? "Dueling");
            }
        }
    }
}
