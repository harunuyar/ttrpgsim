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
using Dnd.System.GameManagers;

public class AttackEvent : AEvent
{
    private BooleanResult? valid;
    private IntegerResultWithBonus? attackRollModifiers, damageModifiers, armorClass;

    public AttackEvent(IEventListener eventListener, IGameActor attacker, IGameActor target, IItem weaponItem, bool versatile) : base(eventListener)
    {
        Attacker = attacker;
        Target = target;
        WeaponItem = weaponItem;
        Versatile = versatile;
    }

    public IGameActor Attacker { get; }

    public IGameActor Target { get; }

    public IItem WeaponItem { get; }

    public bool Versatile { get; }

    public override BooleanResult IsValid()
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

        valid = canDoWeaponAttackAgainstResult;
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

    public int GetSuccessPercentage()
    {
        var attackRollModifiers = GetAttackRollModifiers();
        if (!attackRollModifiers.IsSuccess)
        {
            return 0;
        }

        var armorClass = GetArmorClass();
        if (!armorClass.IsSuccess)
        {
            return 0;
        }

        return Probability.ForAttackRoll(attackRollModifiers.Value, attackRollModifiers.Advantage, armorClass.Value).ToPercentage();
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

        return BooleanResult.Success("Result", attackRollWithoutModifiers + attackRollModifiers.Value >= armorClass.Value);
    }

    public RollResult RollDamageDice()
    {
        if (WeaponItem.ItemDescription is not IWeapon weapon)
        {
            return RollResult.Failure("Item is not a weapon");
        }

        if (weapon.DamageCalculationType == EDamageCalculationType.Constant)
        {
            return RollResult.Success(new int[] { weapon.ConstantDamage });
        }

        var damageDie = Versatile ? weapon.VersatileDamageDie ?? weapon.DamageDie : weapon.DamageDie;
        if (damageDie == null)
        {
            return RollResult.Failure("Damage die is null. Check weapon properties.");
        }

        IntegerResultWithBonus damageModifiers = GetWeaponDamageModifiers();
        var rollAttack = new RollDamage(EventListener, Attacker, damageModifiers.BonusCollection.Advantage, damageDie);
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
            var heal = new HealEvent(EventListener, Target, -damage);
            return heal.QuickRun();
        }
        else
        {
            var applyDamage = new ApplyDamage(EventListener, Target, damage, ((IWeapon)WeaponItem.ItemDescription).DamageType);
            return applyDamage.Execute();
        }
    }

    public override EventResult QuickRun()
    {
        var isValid = IsValid();
        if (!isValid.IsSuccess)
        {
            return EventResult.Failure("IsValid failed: " + isValid.ErrorMessage);
        }

        if (!isValid.Value)
        {
            return EventResult.Failure("Attack is not valid: " + isValid.ErrorMessage);
        }

        var attackRoll = RollAttackDice();
        if (!attackRoll.IsSuccess)
        {
            return EventResult.Failure("RollAttackDice failed: " + attackRoll.ErrorMessage);
        }

        var canHit = CanHit(attackRoll.Value);
        if (!canHit.IsSuccess)
        {
            return EventResult.Failure("CanHit failed: " + canHit.ErrorMessage);
        }

        if (!canHit.Value)
        {
            return EventResult.Success("Attack missed");
        }

        var damageRoll = RollDamageDice();
        if (!damageRoll.IsSuccess)
        {
            return EventResult.Failure("RollDamageDice failed: " + damageRoll.ErrorMessage);
        }

        var totalDamage = GetTotalDamage(damageRoll.Value);
        if (!totalDamage.IsSuccess)
        {
            return EventResult.Failure("GetTotalDamage failed: " + totalDamage.ErrorMessage);
        }

        return ApplyDamage(totalDamage.Value);
    }
}
