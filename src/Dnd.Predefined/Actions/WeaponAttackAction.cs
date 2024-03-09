namespace Dnd.Predefined.Actions;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.AbilityScore;
using Dnd._5eSRD.Models.DamageType;
using Dnd._5eSRD.Models.Equipment;
using Dnd.Context;
using Dnd.Predefined.Commands.BoolCommands;
using Dnd.Predefined.Commands.DamageBonusCommands;
using Dnd.Predefined.Commands.RollBonusCommands;
using Dnd.Predefined.Commands.ScoreCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.Instances;
using Dnd.System.Entities.Units;
using Dnd.System.GameManagers.Dice;

public class WeaponAttackAction : AttackRollAction, IWeaponAttackAction
{
    public static Task<WeaponAttackAction> CreateMainHandWeaponAttack(IEquipmentInstance weapon) => Create(ActionDurationType.Action, weapon, EAttackHandType.MainHand);
    public static Task<WeaponAttackAction> CreateOffHandWeaponAttack(IEquipmentInstance weapon) => Create(ActionDurationType.BonusAction, weapon, EAttackHandType.OffHand);
    public static Task<WeaponAttackAction> CreateVersatileWeaponAttack(IEquipmentInstance weapon) => Create(ActionDurationType.Action, weapon, EAttackHandType.Versatile);

    public static async Task<WeaponAttackAction> Create(ActionDurationType actionDurationType, IEquipmentInstance weapon, EAttackHandType attackHandType)
    {
        var damageType = await DndContext.Instance.GetObject<DamageTypeModel>(weapon.EquipmentModel.Damage?.DamageType?.Url);
        return damageType == null
            ? throw new InvalidOperationException($"Weapon damage type model {weapon.EquipmentModel.Damage?.DamageType?.Url} is not found")
            : new WeaponAttackAction(actionDurationType, damageType, weapon, attackHandType);
    }

    public WeaponAttackAction(ActionDurationType actionDurationType, DamageTypeModel damageType, IEquipmentInstance weapon, EAttackHandType attackHandType)
        : base("Weapon Attack", actionDurationType, 
            weapon.EquipmentModel.WeaponRange == EWeaponRange.Melee 
                ? ActionRange.Melee(Distance.OfFeet(weapon.EquipmentModel.Range?.Normal ?? 0), weapon.EquipmentModel.Range?.Long is not null ? Distance.OfFeet(weapon.EquipmentModel.Range.Long.Value) : null)
                : ActionRange.Ranged(Distance.OfFeet(weapon.EquipmentModel.Range?.Normal ?? 0), weapon.EquipmentModel.Range?.Long is not null ? Distance.OfFeet(weapon.EquipmentModel.Range.Long.Value) : null),
            TargetingType.SingleTarget, damageType, DicePool.Parse(attackHandType == EAttackHandType.Versatile ? weapon.EquipmentModel.TwoHandedDamage?.DamageDice : weapon.EquipmentModel.Damage?.DamageDice), [])
    {
        Weapon = weapon;
        HandType = attackHandType;
    }

    public IEquipmentInstance Weapon { get; }

    public EAttackHandType HandType { get; }

    public override async Task HandleUsageCommand(ICommand command)
    {
        await base.HandleUsageCommand(command);

        await Weapon.HandleUsageCommand(command);

        if (command is GetModifiers modifiers)
        {
            if (Range.RangeType == ERangeType.Ranged)
            {
                var dex = await DndContext.Instance.GetObject<AbilityScoreModel>(AbilityScores.Dex);

                if (dex == null)
                {
                    modifiers.SetError("Dexterity score model is not found.");
                    return;
                }

                var dexModifier = await new GetAbilityModifier(command.Actor, dex).Execute();

                if (!dexModifier.IsSuccess)
                {
                    modifiers.SetError("GetAbilityModifier: " + dexModifier.ErrorMessage);
                    return;
                }

                modifiers.AddValue(dexModifier.Value, "Dexterity");
            }
            else
            {
                var str = await DndContext.Instance.GetObject<AbilityScoreModel>(AbilityScores.Str);

                if (str == null)
                {
                    modifiers.SetError("Strength score model is not found.");
                    return;
                }

                var strengthModifier = await new GetAbilityModifier(command.Actor, str).Execute();

                if (!strengthModifier.IsSuccess)
                {
                    modifiers.SetError("GetAbilityModifier: " + strengthModifier.ErrorMessage);
                    return;
                }

                if (Weapon.EquipmentModel.Properties?.Any(p => p.Url == WeaponProperties.Finesse) ?? false)
                {
                    var dex = await DndContext.Instance.GetObject<AbilityScoreModel>(AbilityScores.Dex);

                    if (dex == null)
                    {
                        modifiers.SetError("Dexterity score model is not found.");
                        return;
                    }

                    var dexModifier = await new GetAbilityModifier(command.Actor, dex).Execute();

                    if (!dexModifier.IsSuccess)
                    {
                        modifiers.SetError("GetAbilityModifier: " + dexModifier.ErrorMessage);
                        return;
                    }

                    if (dexModifier.Value > strengthModifier.Value)
                    {
                        modifiers.AddValue(dexModifier.Value, "Dexterity");
                    }
                    else
                    {
                        modifiers.AddValue(strengthModifier.Value, "Strength");
                    }
                }
                else
                {
                    modifiers.AddValue(strengthModifier.Value, "Strength");
                }
            }

            var hasProficiency = await new HasProficiency(command.Actor, Weapon.EquipmentModel).Execute();

            if (!hasProficiency.IsSuccess)
            {
                modifiers.SetError("HasProficiency: " + hasProficiency.ErrorMessage);
                return;
            }

            if (hasProficiency.Value)
            {
                var proficiency = await new GetProficiencyBonus(command.Actor).Execute();

                if (!proficiency.IsSuccess)
                {
                    modifiers.SetError("GetProficiencyBonus: " + proficiency.ErrorMessage);
                    return;
                }

                modifiers.AddValue(proficiency.Value, "Proficiency");
            }
        }
        else if (command is GetAmountModifiers amountModifiers)
        {
            if (HandType != EAttackHandType.OffHand)
            {
                if (Range.RangeType == ERangeType.Ranged)
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

                    if (Weapon.EquipmentModel.Properties?.Any(p => p.Url == WeaponProperties.Finesse) ?? false)
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
