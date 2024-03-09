namespace Dnd.Predefined.Definitions.Features.Fighter;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.AbilityScore;
using Dnd._5eSRD.Models.Feature;
using Dnd.Context;
using Dnd.Predefined.Commands.DamageBonusCommands;
using Dnd.Predefined.Commands.ScoreCommands;
using Dnd.Predefined.Instances;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;

public class FightingStyleTwoWeaponFighting : FeatureInstance, ISubFightingStyle
{
    public static Task<FightingStyleTwoWeaponFighting> Create()
    {
        return DndContext.Instance.GetObject<FeatureModel>(Features.FighterFightingStyleTwoWeaponFighting).ContinueWith(
            t => t.Result == null
                ? throw new InvalidOperationException("FighterFightingStyleTwoWeaponFighting feature model is not found")
                : new FightingStyleTwoWeaponFighting(t.Result));
    }

    private FightingStyleTwoWeaponFighting(FeatureModel featureModel) : base(featureModel)
    {
    }

    public override async Task HandleCommand(ICommand command)
    {
        await base.HandleCommand(command);

        if (command is GetAmountModifiers amountModifiers)
        {
            if (amountModifiers.AmountAction is IWeaponAttackAction weaponAttack && weaponAttack.HandType == EAttackHandType.OffHand)
            {
                if (weaponAttack.Range.RangeType == ERangeType.Ranged)
                {
                    var dex = await DndContext.Instance.GetObject<AbilityScoreModel>(AbilityScores.Dex);

                    if (dex == null)
                    {
                        amountModifiers.SetError("Dexterity score model is not found.");
                        return;
                    }

                    var dexModifier = await new GetAbilityModifier(command.Actor, dex).Execute();

                    if (!dexModifier.IsSuccess)
                    {
                        amountModifiers.SetError("GetAbilityModifier: " + dexModifier.ErrorMessage);
                        return;
                    }

                    amountModifiers.AddValue(dexModifier.Value, "Dexterity");
                }
                else
                {
                    var str = await DndContext.Instance.GetObject<AbilityScoreModel>(AbilityScores.Str);

                    if (str == null)
                    {
                        amountModifiers.SetError("Strength score model is not found.");
                        return;
                    }

                    var strengthModifier = await new GetAbilityModifier(command.Actor, str).Execute();

                    if (!strengthModifier.IsSuccess)
                    {
                        amountModifiers.SetError("GetAbilityModifier: " + strengthModifier.ErrorMessage);
                        return;
                    }

                    if (weaponAttack.Weapon.EquipmentModel.Properties?.Any(p => p.Url == WeaponProperties.Finesse) ?? false)
                    {
                        var dex = await DndContext.Instance.GetObject<AbilityScoreModel>(AbilityScores.Dex);

                        if (dex == null)
                        {
                            amountModifiers.SetError("Dexterity score model is not found.");
                            return;
                        }

                        var dexModifier = await new GetAbilityModifier(command.Actor, dex).Execute();

                        if (!dexModifier.IsSuccess)
                        {
                            amountModifiers.SetError("GetAbilityModifier: " + dexModifier.ErrorMessage);
                            return;
                        }

                        if (dexModifier.Value > strengthModifier.Value)
                        {
                            amountModifiers.AddValue(dexModifier.Value, "Dexterity");
                        }
                        else
                        {
                            amountModifiers.AddValue(strengthModifier.Value, "Strength");
                        }
                    }
                    else
                    {
                        amountModifiers.AddValue(strengthModifier.Value, "Strength");
                    }
                }
            }
        }
    }
}
