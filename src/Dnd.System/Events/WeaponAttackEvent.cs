namespace Dnd.System.Events;

using Dnd.System.CommandSystem.Commands.BooleanResultCommands;
using Dnd.System.CommandSystem.Commands.EventCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
using Dnd.System.CommandSystem.Commands.RollCommands;
using Dnd.System.CommandSystem.Commands.ValueCommands;
using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities;
using Dnd.System.Entities.Advantage;
using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Items;
using Dnd.System.Entities.Items.Equipments.Weapons;
using Dnd.System.Events.EventListener;
using Dnd.System.GameManagers;

public class WeaponAttackEvent : AEvent
{
    private BooleanResult? valid;
    private IntegerResultWithBonus? attackRollModifiers, damageModifiers, armorClass;

    public WeaponAttackEvent(IEventListener eventListener, IGameActor attacker, IGameActor target, IItem weaponItem, bool versatile) : base(eventListener)
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

        var canAttack = new CanAttackTarget(Attacker, WeaponItem, Target).Execute();

        if (!canAttack.IsSuccess)
        {
            return BooleanResult.Failure("CanAttackTarget failed: " + canAttack.ErrorMessage);
        }

        if (!canAttack.Value)
        {
            return canAttack;
        }

        var canBeTargeted = new CanBeTargeted(Target, Attacker).Execute();

        if (!canBeTargeted.IsSuccess)
        {
            return BooleanResult.Failure("CanBeTargeted failed: " + canBeTargeted.ErrorMessage);
        }

        if (!canBeTargeted.Value)
        {
            return canBeTargeted;
        }

        valid = canBeTargeted;
        return valid;
    }

    public IntegerResultWithBonus GetAttackRollModifiers()
    {
        if (attackRollModifiers != null)
        {
            return attackRollModifiers;
        }

        var attackModifiers = new GetWeaponAttackModifier(Attacker, WeaponItem, Target).Execute();

        if (!attackModifiers.IsSuccess)
        {
            return IntegerResultWithBonus.Failure("CalculateWeaponAttackModifier failed: " + attackModifiers.ErrorMessage);
        }

        var attackModifiersAgainst = new GetWeaponAttackModifierAgainst(Target, WeaponItem, Attacker).Execute();

        if (!attackModifiersAgainst.IsSuccess)
        {
            return IntegerResultWithBonus.Failure("CalculateWeaponAttackModifierAgainst failed: " + attackModifiersAgainst.ErrorMessage);
        }

        attackModifiers.AddAsBonus(new CustomDndEntity("Target Base"), attackModifiersAgainst);

        attackRollModifiers = attackModifiers;
        return attackRollModifiers;
    }

    public IntegerResultWithBonus GetWeaponDamageModifiers()
    {
        if (damageModifiers != null)
        {
            return damageModifiers;
        }

        var calculateWeaponDamageModifiers = new GetWeaponDamageModifier(Attacker, WeaponItem, Target);
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

        if (attackRollModifiers.RollResult.IsSuccess())
        {
            return 100;
        }

        if (attackRollModifiers.RollResult.IsFailure())
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

    public ValueResult<ERollResult> GetRollResult(int roll)
    {
        var armorClass = GetArmorClass();

        if (!armorClass.IsSuccess)
        {
            return ValueResult<ERollResult>.Failure("GetArmorClass failed: " + armorClass.ErrorMessage);
        }

        var attackRollModifiers = GetAttackRollModifiers();

        if (!attackRollModifiers.IsSuccess)
        {
            return ValueResult<ERollResult>.Failure("GetArmorClass failed: " + armorClass.ErrorMessage);
        }

        var rollSuccess = new GetRollSuccess(Attacker, armorClass.Value, roll, attackRollModifiers.Value).Execute();

        return rollSuccess;
    }

    public RollResult RollDamageDice(ERollResult attackRollSuccess)
    {
        if (WeaponItem.ItemDescription is not IWeapon weapon)
        {
            return RollResult.Failure("Item is not a weapon.");
        }

        if (attackRollSuccess.IsFailure())
        {
            return RollResult.Empty();
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
        
        var rollAttack = new RollDamage(EventListener, Attacker, damageModifiers.Advantage, damageDie, attackRollSuccess.IsCriticalSuccess());
        var rollResult = rollAttack.Execute();

        if (!rollResult.IsSuccess)
        {
            return RollResult.Failure("RollDamage failed: " + rollResult.ErrorMessage);
        }

        return rollResult;
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

        var calculateDamageResult = new CalculateDamage(Target, damageRollWithoutModifiers + damageModifiers.Value, ((IWeapon)WeaponItem.ItemDescription).DamageType).Execute();

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

        var attackRollModifiers = GetAttackRollModifiers();

        if (!attackRollModifiers.IsSuccess)
        {
            return EventResult.Failure("GetAttackRollModifiers failed: " + attackRollModifiers.ErrorMessage);
        }

        ERollResult rollResult = attackRollModifiers.RollResult;

        if (rollResult.IsFailure())
        {
            return EventResult.Success("Attack missed due to critical failure");
        }

        if (!rollResult.IsSuccess())
        {
            var attackRoll = RollAttackDice();
            if (!attackRoll.IsSuccess)
            {
                return EventResult.Failure("RollAttackDice failed: " + attackRoll.ErrorMessage);
            }

            var rollCommandResult = GetRollResult(attackRoll.Value);

            if (!rollCommandResult.IsSuccess)
            {
                return EventResult.Failure("GetRollResult failed: " + rollCommandResult.ErrorMessage);
            }

            rollResult = rollCommandResult.Value;
        }

        var damageRoll = RollDamageDice(rollResult);
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
