namespace Dnd.System.Events;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.CommandSystem.Commands.BooleanResultCommands;
using Dnd.System.CommandSystem.Commands.EventCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
using Dnd.System.CommandSystem.Commands.RollCommands;
using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities;
using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Items;
using Dnd.System.Entities.Items.Equipments.Weapons;

public class AttackEvent : IDndEntity
{
    private BooleanResult? valid;
    private IntegerResultWithBonus? attackRollModifiers, damageModifiers, armorClass;

    public AttackEvent(IEventListener eventListener, IGameActor attacker, IGameActor target, IItem weaponItem)
    {
        EventListener = eventListener;
        Attacker = attacker;
        Target = target;
        WeaponItem = weaponItem;
    }

    public IEventListener EventListener { get; }

    public IGameActor Attacker { get; }

    public IGameActor Target { get; }

    public IItem WeaponItem { get; }

    public string Name => "Attack Event";

    private BooleanResult IsValid()
    {
        if (valid != null)
        {
            return valid;
        }

        if (WeaponItem.ItemDescription is not IWeapon)
        {
            valid = BooleanResult.Failure("Item is not a weapon");
            return valid;
        }

        var canDoWeaponAttack = new CanDoWeaponAttack(Attacker, WeaponItem, Target);
        var canDoWeaponAttackResult = canDoWeaponAttack.Execute();

        if (!canDoWeaponAttackResult.IsSuccess)
        {
            return BooleanResult.Failure("CanDoWeaponAttack failed: " + canDoWeaponAttackResult.ErrorMessage);
        }

        if (!canDoWeaponAttackResult.Value)
        {
            return canDoWeaponAttackResult;
        }

        var canDoWeaponAttackAgainst = new CanDoWeaponAttackAgainst(Target, WeaponItem, Attacker);
        var canDoWeaponAttackAgainstResult = canDoWeaponAttackAgainst.Execute();

        if (!canDoWeaponAttackAgainstResult.IsSuccess)
        {
            return BooleanResult.Failure("CanDoWeaponAttackAgainst failed: " + canDoWeaponAttackAgainstResult.ErrorMessage);
        }

        if (!canDoWeaponAttackAgainstResult.Value)
        {
            return canDoWeaponAttackAgainstResult;
        }

        valid = BooleanResult.Success(this, true);
        return valid;
    }

    public IntegerResultWithBonus GetAttackRollModifiers()
    {
        if (attackRollModifiers != null)
        {
            return attackRollModifiers;
        }

        var calculateWeaponAttackModifiers = new CalculateWeaponAttackModifier(Attacker, WeaponItem, Target);
        var calculateWeaponAttackModifiersResult = calculateWeaponAttackModifiers.Execute();

        if (!calculateWeaponAttackModifiersResult.IsSuccess)
        {
            return IntegerResultWithBonus.Failure("CalculateWeaponAttackModifier failed: " + calculateWeaponAttackModifiersResult.ErrorMessage);
        }

        var calculateWeaponAttackAgainstModifiers = new CalculateWeaponAttackModifierAgainst(Target, WeaponItem, Attacker);
        var calculateWeaponAttackAgainstModifiersResult = calculateWeaponAttackAgainstModifiers.Execute();

        if (!calculateWeaponAttackAgainstModifiersResult.IsSuccess)
        {
            return IntegerResultWithBonus.Failure("CalculateWeaponAttackModifierAgainst failed: " + calculateWeaponAttackAgainstModifiersResult.ErrorMessage);
        }

        foreach (var pair in calculateWeaponAttackAgainstModifiersResult.BonusCollection.Advantages)
        {
            calculateWeaponAttackModifiersResult.BonusCollection.Advantages.Add(pair.Key, pair.Value);
        }

        foreach (var pair in calculateWeaponAttackAgainstModifiersResult.BonusCollection.Values)
        {
            calculateWeaponAttackModifiersResult.BonusCollection.Values.Add(pair.Key, pair.Value);
        }

        if (calculateWeaponAttackAgainstModifiersResult.BaseValue != 0)
        {
            calculateWeaponAttackModifiersResult.BonusCollection.Values.Add(
                calculateWeaponAttackAgainstModifiersResult.BaseSource ?? new CustomDndEntity("Target Base"), 
                calculateWeaponAttackAgainstModifiersResult.BaseValue);
        }

        attackRollModifiers = calculateWeaponAttackModifiersResult;
        return attackRollModifiers;
    }

    public IntegerResultWithBonus GetWeaponDamageModifiers()
    {
        if (damageModifiers != null)
        {
            return damageModifiers;
        }

        var calculateWeaponDamageModifiers = new CalculateWeaponDamageModifier(Attacker, WeaponItem, Target);
        var calculateWeaponDamageModifiersResult = calculateWeaponDamageModifiers.Execute();

        if (!calculateWeaponDamageModifiersResult.IsSuccess)
        {
            return IntegerResultWithBonus.Failure("CalculateWeaponDamageModifier failed: " + calculateWeaponDamageModifiersResult.ErrorMessage);
        }

        damageModifiers = calculateWeaponDamageModifiersResult;
        return damageModifiers;
    }

    public IntegerResultWithBonus GetArmorClass()
    {
        if (armorClass != null)
        {
            return armorClass;
        }

        var getArmorClass = new GetArmorClass(Target);
        var getArmorClassResult = getArmorClass.Execute();

        if (!getArmorClassResult.IsSuccess)
        {
            return IntegerResultWithBonus.Failure("GetArmorClass failed: " + getArmorClassResult.ErrorMessage);
        }

        armorClass = getArmorClassResult;
        return armorClass;
    }

    public RollResult RollAttackDice()
    {
        IntegerResultWithBonus attackRollModifiers = GetAttackRollModifiers();
        var rollAttack = new RollAttack(EventListener, Attacker, attackRollModifiers.BonusCollection.Advantage);
        return rollAttack.Execute();
    }

    public BooleanResult CanHit(int attackRollWithoutModifiers)
    {
        var isValid = IsValid();
        if (!isValid.IsSuccess)
        {
            return BooleanResult.Failure("IsValid failed: " + isValid.ErrorMessage);
        }

        var attackRollModifiers = GetAttackRollModifiers();
        if (!attackRollModifiers.IsSuccess)
        {
            return BooleanResult.Failure("GetAttackRollModifiers failed: " + attackRollModifiers.ErrorMessage);
        }

        var armorClass = GetArmorClass();
        if (!armorClass.IsSuccess)
        {
            return BooleanResult.Failure("GetArmorClass failed: " + armorClass.ErrorMessage);
        }

        return BooleanResult.Success(this, attackRollWithoutModifiers + attackRollModifiers.Value >= armorClass.Value);
    }

    public RollResult RollDamageDice()
    {
        IntegerResultWithBonus damageModifiers = GetWeaponDamageModifiers();
        var rollAttack = new RollAttack(EventListener, Attacker, damageModifiers.BonusCollection.Advantage);
        return rollAttack.Execute();
    }

    public IntegerResultWithBonus GetTotalDamage(int damageRollWithoutModifiers)
    {
        var isValid = IsValid();
        if (!isValid.IsSuccess)
        {
            return IntegerResultWithBonus.Failure("IsValid failed: " + isValid.ErrorMessage);
        }

        var damageModifiers = GetWeaponDamageModifiers();
        if (!damageModifiers.IsSuccess)
        {
            return IntegerResultWithBonus.Failure("GetWeaponDamageModifier failed: " + damageModifiers.ErrorMessage);
        }

        var calculateDamage = new CalculateDamage(Target, damageRollWithoutModifiers + damageModifiers.Value, ((IWeapon)WeaponItem.ItemDescription).DamageType);
        var calculateDamageResult = calculateDamage.Execute();

        if (!calculateDamageResult.IsSuccess)
        {
            return IntegerResultWithBonus.Failure("CalculateDamage failed: " + calculateDamageResult.ErrorMessage);
        }

        return calculateDamageResult;
    }

    public EventResult ApplyDamage(int damage)
    {
        if (damage < 0)
        {
            var heal = new ApplyHeal(EventListener, Target, damage);
            return heal.Execute();
        }
        else
        {
            var applyDamage = new ApplyDamage(EventListener, Target, damage, ((IWeapon)WeaponItem.ItemDescription).DamageType);
            return applyDamage.Execute();
        }
    }
}
