namespace Dnd.Predefined.Actions;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.AbilityScore;
using Dnd._5eSRD.Models.DamageType;
using Dnd.Context;
using Dnd.Predefined.Commands.BoolCommands;
using Dnd.Predefined.Commands.DamageBonusCommands;
using Dnd.Predefined.Commands.RollBonusCommands;
using Dnd.Predefined.Commands.ScoreCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;
using Dnd.System.Entities.Instances;
using Dnd.System.GameManagers.Dice;

public class WeaponAttackAction : AttackAction, IWeaponAttackAction
{
    public static async Task<WeaponAttackAction> Create(IGameActor actionOwner, ActionDurationType actionDurationType, IEquipmentInstance weapon, EAttackHandType attackHandType)
    {
        var damageType = await DndContext.Instance.GetObject<DamageTypeModel>(weapon.EquipmentModel.Damage?.DamageType?.Url);
        return damageType == null
            ? throw new InvalidOperationException("Bludgeoning damage type model is not found")
            : new WeaponAttackAction(actionOwner, actionDurationType, damageType, weapon, attackHandType);
    }

    public WeaponAttackAction(IGameActor actionOwner, ActionDurationType actionDurationType, DamageTypeModel damageType, IEquipmentInstance weapon, EAttackHandType attackHandType)
        : base(actionOwner, "Weapon Attack", actionDurationType, ActionRange.FromString(weapon.EquipmentModel.WeaponRange) ?? ActionRange.Touch, TargetingType.SingleTarget, damageType, 
            DicePool.Parse(attackHandType == EAttackHandType.Versatile ? weapon.EquipmentModel.TwoHandedDamage?.DamageDice : weapon.EquipmentModel.Damage?.DamageDice))
    {
        SuccessRollAction = new SuccessRollAction(actionOwner, Name, actionDurationType, ERollType.Attack);
        Weapon = weapon;
        HandType = attackHandType;
    }

    private SuccessRollAction SuccessRollAction { get; }

    public ERollType RollType => SuccessRollAction.RollType;

    public IEquipmentInstance Weapon { get; }

    public EAttackHandType HandType { get; }

    public Task<ERollResult> GetPredeterminedResult()
    {
        return SuccessRollAction.GetPredeterminedResult();
    }

    public Task<ERollResult> GetResult(ERollResult defaultResult)
    {
        return SuccessRollAction.GetResult(defaultResult);
    }

    public Task<EAdvantage> GetRollAdvantage()
    {
        return SuccessRollAction.GetRollAdvantage();
    }

    public Task<DicePool> GetRollBonus()
    {
        return SuccessRollAction.GetRollBonus();
    }

    public override async Task HandleUsageCommand(ICommand command)
    {
        await base.HandleUsageCommand(command);

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

                var dexModifier = await new GetAbilityModifier(ActionOwner, dex).Execute();

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

                var strengthModifier = await new GetAbilityModifier(ActionOwner, str).Execute();

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

                    var dexModifier = await new GetAbilityModifier(ActionOwner, dex).Execute();

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

            var hasProficiency = await new HasProficiency(ActionOwner, Weapon.EquipmentModel).Execute();

            if (!hasProficiency.IsSuccess)
            {
                modifiers.SetError("HasProficiency: " + hasProficiency.ErrorMessage);
                return;
            }

            if (hasProficiency.Value)
            {
                var proficiency = await new GetProficiencyBonus(ActionOwner).Execute();

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
                var str = await DndContext.Instance.GetObject<AbilityScoreModel>(AbilityScores.Str);

                if (str == null)
                {
                    amountModifiers.SetError("Strength score model is not found.");
                    return;
                }

                var strengthModifier = await new GetAbilityModifier(ActionOwner, str).Execute();

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

                    var dexModifier = await new GetAbilityModifier(ActionOwner, dex).Execute();

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
