﻿namespace Dnd.Predefined.Actions.ActionTypes;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.AbilityScore;
using Dnd._5eSRD.Models.DamageType;
using Dnd.Context;
using Dnd.Predefined.Commands.DamageBonusCommands;
using Dnd.Predefined.Commands.RollBonusCommands;
using Dnd.Predefined.Commands.ScoreCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.GameManagers.Dice;

public class UnarmedAttackAction : AttackRollDamageAction, IUnarmedAttackAction
{
    public static Task<UnarmedAttackAction> CreateMainHandUnarmedAttack() => Create(ActionDurationType.Action, EAttackHandType.MainHand);
    public static Task<UnarmedAttackAction> CreateOffHandUnarmedAttack() => Create(ActionDurationType.BonusAction, EAttackHandType.OffHand);

    public static async Task<UnarmedAttackAction> Create(ActionDurationType actionDurationType, EAttackHandType handType)
    {
        var damageType = await DndContext.Instance.GetObject<DamageTypeModel>(DamageTypes.Bludgeoning);

        return damageType == null
            ? throw new InvalidOperationException("Bludgeoning damage type model is not found")
            : new UnarmedAttackAction(actionDurationType, damageType, handType);
    }

    public UnarmedAttackAction(ActionDurationType actionDurationType, DamageTypeModel damageType, EAttackHandType handType)
        : base("Unarmed Attack", actionDurationType, [], ActionRange.Touch, TargetingType.SingleTarget, damageType, new DicePool([], 1))
    {
        HandType = handType;
    }

    public EAttackHandType HandType { get; }

    public override async Task HandleUsageCommand(ICommand command)
    {
        await base.HandleUsageCommand(command);

        if (command is GetModifiers modifiers)
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

            var proficiency = await new GetProficiencyBonus(command.Actor).Execute();

            if (!proficiency.IsSuccess)
            {
                modifiers.SetError("GetProficiencyBonus: " + proficiency.ErrorMessage);
                return;
            }

            modifiers.AddValue(proficiency.Value, "Proficiency");
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

                var strengthModifier = await new GetAbilityModifier(command.Actor, str).Execute();

                if (!strengthModifier.IsSuccess)
                {
                    amountModifiers.SetError("GetAbilityModifier: " + strengthModifier.ErrorMessage);
                    return;
                }

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
        }
    }
}
