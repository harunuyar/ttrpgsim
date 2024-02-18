namespace Dnd.System.Entities.Actions.Impl;

using Dnd.GameManagers.Dice;
using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.CommandSystem.Commands.BooleanResultCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands.Modifiers;
using Dnd.System.Entities.Actions.BaseActions;
using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.Damage;
using Dnd.System.Entities.Items.Equipments.Weapons;

public class WeaponAttack : ATouchAction, IAttackAction
{
    public WeaponAttack(IWeapon weapon, EActionType actionType, bool versatile) 
        : base($"Attack with {weapon.Name}", $"You can attack with {weapon.Name} using your {actionType}.", actionType, [new UsageLimitation(EUsageLimitation.None, null)])
    {
        Weapon = weapon;
        IsVersatile = versatile;
    }

    public IWeapon Weapon { get; }

    public bool IsVersatile { get; }

    public EDamageType DamageType => Weapon.DamageType;

    public ESuccessMeasuringType SuccessMeasuringType => Weapon.SuccessMeasuringType;

    public EDamageCalculationType DamageCalculationType => Weapon.DamageCalculationType;

    public int? ConstantDamage => Weapon.ConstantDamage;

    public DiceRoll? DamageDie => IsVersatile ? Weapon.VersatileDamageDie ?? Weapon.DamageDie : Weapon.DamageDie;

    public EAttributeType? SavingThrowAttribute => null;

    public Func<int, int>? FailureDamageModifier => null;

    public override void HandleCommand(ICommand command)
    {
        base.HandleCommand(command);

        if (command is GetAttackModifier getAttackModifier)
        {
            if (getAttackModifier.AttackAction != this)
            {
                return;
            }

            if (SuccessMeasuringType != ESuccessMeasuringType.AttackRoll)
            {
                return;
            }

            var strengthModifier = new GetAttributeModifier(command.Actor, EAttributeType.Strength).Execute();

            if (!strengthModifier.IsSuccess)
            {
                getAttackModifier.SetErrorAndReturn("GetAttributeModifier: " + strengthModifier.ErrorMessage);
                return;
            }

            IAttribute usedAttribute = command.Actor.AttributeSet.GetAttribute(EAttributeType.Strength);
            var attributeModifier = strengthModifier;

            if (Weapon.WeaponProperties.HasFlag(EWeaponProperty.Finesse | EWeaponProperty.Range))
            {
                var getDexterityModifier = new GetAttributeModifier(command.Actor, EAttributeType.Dexterity);
                var dexterityModifier = getDexterityModifier.Execute();

                if (!dexterityModifier.IsSuccess)
                {
                    getAttackModifier.SetErrorAndReturn("GetAttributeModifier: " + dexterityModifier.ErrorMessage);
                    return;
                }

                if (dexterityModifier.Value > attributeModifier.Value)
                {
                    attributeModifier = dexterityModifier;
                    usedAttribute = command.Actor.AttributeSet.GetAttribute(EAttributeType.Dexterity);
                }
            }

            getAttackModifier.Result.AddAsBonus(usedAttribute, attributeModifier);

            var hasProficiency = new HasWeaponProficiency(command.Actor, Weapon.WeaponType).Execute();

            if (!hasProficiency.IsSuccess)
            {
                getAttackModifier.SetErrorAndReturn("HasWeaponProficiency: " + hasProficiency.ErrorMessage);
                return;
            }

            if (hasProficiency.Value)
            {
                var proficiencyBonus = new GetProficiencyBonus(command.Actor).Execute();

                if (!proficiencyBonus.IsSuccess)
                {
                    getAttackModifier.SetErrorAndReturn("GetProficiencyBonus: " + proficiencyBonus.ErrorMessage);
                    return;
                }

                getAttackModifier.Result.AddAsBonus("Proficiency Bonus", proficiencyBonus);
            }
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

            IAttribute usedAttribute = command.Actor.AttributeSet.GetAttribute(EAttributeType.Strength);
            var attributeModifier = strengthModifier;

            if (Weapon.WeaponProperties.HasFlag(EWeaponProperty.Finesse | EWeaponProperty.Range))
            {
                var dexterityModifier = new GetAttributeModifier(command.Actor, EAttributeType.Dexterity).Execute();

                if (!dexterityModifier.IsSuccess)
                {
                    getDamageModifier.SetErrorAndReturn("GetAttributeModifier: " + dexterityModifier.ErrorMessage);
                    return;
                }

                if (dexterityModifier.Value > attributeModifier.Value)
                {
                    attributeModifier = dexterityModifier;
                    usedAttribute = command.Actor.AttributeSet.GetAttribute(EAttributeType.Dexterity);
                }
            }

            if (ActionType == EActionType.MainAction || attributeModifier.Value < 0)
            {
                getDamageModifier.Result.AddAsBonus(usedAttribute, attributeModifier);
            }
        }
    }
}
