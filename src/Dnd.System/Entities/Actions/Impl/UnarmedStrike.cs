namespace Dnd.System.Entities.Actions.Impl;

using Dnd.GameManagers.Dice;
using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands.Modifiers;
using Dnd.System.Entities.Actions;
using Dnd.System.Entities.Actions.BaseActions;
using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.Damage;

public class UnarmedStrike : ATouchAction, IAttackAction
{
    public UnarmedStrike(EActionType actionType) 
        : base("Unarmed Strike", "A punch, kick, head-butt, or similar forceful blow", actionType, [new UsageLimitation(EUsageLimitation.None, null)])
    {
    }

    public EDamageType DamageType => EDamageType.Bludgeoning;

    public ESuccessMeasuringType SuccessMeasuringType => ESuccessMeasuringType.AttackRoll;

    public EDamageCalculationType DamageCalculationType => EDamageCalculationType.Constant;

    public int? ConstantDamage => 1; // + Strength modifier

    public DiceRoll? DamageDie => null;

    public EAttributeType? SavingThrowAttribute => null;

    public Func<int, int>? FailureDamageModifier => null;

    public override void HandleCommand(ICommand command)
    {
        base.HandleCommand(command);

        if (command is GetAttackModifier getPossibleActions)
        {
            if (getPossibleActions.AttackAction != this)
            {
                return;
            }

            var strengthModifier = new GetAttributeModifier(command.Actor, EAttributeType.Strength).Execute();

            if (!strengthModifier.IsSuccess)
            {
                getPossibleActions.SetErrorAndReturn("GetAttributeModifier: " + strengthModifier.ErrorMessage);
                return;
            }

            IAttribute usedAttribute = command.Actor.AttributeSet.GetAttribute(EAttributeType.Strength);
            var attributeModifier = strengthModifier;

            var dexterityModifier = new GetAttributeModifier(command.Actor, EAttributeType.Dexterity).Execute();

            if (!dexterityModifier.IsSuccess)
            {
                getPossibleActions.SetErrorAndReturn("GetAttributeModifier: " + dexterityModifier.ErrorMessage);
                return;
            }

            if (dexterityModifier.Value > attributeModifier.Value)
            {
                attributeModifier = dexterityModifier;
                usedAttribute = command.Actor.AttributeSet.GetAttribute(EAttributeType.Dexterity);
            }

            getPossibleActions.Result.AddAsBonus(usedAttribute, attributeModifier);

            var proficiencyBonus = new GetProficiencyBonus(command.Actor).Execute();

            if (!proficiencyBonus.IsSuccess)
            {
                getPossibleActions.SetErrorAndReturn("GetProficiencyBonus: " + proficiencyBonus.ErrorMessage);
                return;
            }

            getPossibleActions.Result.AddAsBonus("Proficiency Bonus", proficiencyBonus);
        }
        else if (command is GetDamageModifier getDamageModifier)
        {
            if (getDamageModifier.AttackAction != this)
            {
                return;
            }

            var strengthModifier = new GetAttributeModifier(command.Actor, EAttributeType.Strength).Execute();

            if (!strengthModifier.IsSuccess)
            {
                getDamageModifier.SetErrorAndReturn("GetAttributeModifier: " + strengthModifier.ErrorMessage);
                return;
            }

            if (ActionType == EActionType.MainAction || strengthModifier.Value < 0)
            {
                getDamageModifier.Result.AddAsBonus("Strength", strengthModifier);
            }
        }
    }
}
