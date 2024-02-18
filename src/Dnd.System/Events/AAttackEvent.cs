namespace Dnd.System.Events;

using Dnd.System.CommandSystem.Commands.EventCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands.Modifiers;
using Dnd.System.CommandSystem.Commands.RollCommands;
using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities.Actions.BaseActions;
using Dnd.System.Entities.Damage;
using Dnd.System.Entities.DiceModifiers;
using Dnd.System.Entities.GameActors;
using Dnd.System.Events.EventListener;

public abstract class AAttackEvent : AEvent
{
    private IntegerResultWithBonus? damageModifiers;

    public AAttackEvent(IEventListener eventListener, IAttackAction attackAction, IGameActor attacker, IGameActor target) : base(eventListener)
    {
        AttackAction = attackAction;
        Attacker = attacker;
        Target = target;
    }

    public IAttackAction AttackAction { get; }

    public IGameActor Attacker { get; }

    public IGameActor Target { get; }

    public abstract int GetSuccessPercentage();

    public IntegerResultWithBonus GetDamageModifiers()
    {
        if (damageModifiers != null)
        {
            return damageModifiers;
        }

        var attackDamageModifiers = new GetDamageModifier(Attacker, Target, AttackAction).Execute();

        if (!attackDamageModifiers.IsSuccess)
        {
            return IntegerResultWithBonus.Failure("GetDamageModifier failed: " + attackDamageModifiers.ErrorMessage);
        }

        damageModifiers = attackDamageModifiers;
        return damageModifiers;
    }

    public RollResult RollDamageDice(ERollResult attackRollSuccess)
    {
        if (attackRollSuccess.IsFailure())
        {
            return RollResult.Empty();
        }

        if (AttackAction.DamageCalculationType == EDamageCalculationType.Constant)
        {
            if (AttackAction.ConstantDamage == null)
            {
                return RollResult.Failure("ConstantDamage is null. Check weapon properties.");
            }

            return RollResult.Success([AttackAction.ConstantDamage!.Value]);
        }

        if (AttackAction.DamageDie == null)
        {
            return RollResult.Failure("DamageDie is null. Check weapon properties.");
        }

        IntegerResultWithBonus damageModifiers = GetDamageModifiers();

        var rollAttack = new RollDamage(EventListener, Attacker, damageModifiers.Advantage, AttackAction.DamageDie, attackRollSuccess.IsCriticalSuccess());
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

        var damageModifiers = GetDamageModifiers();
        if (!damageModifiers.IsSuccess)
        {
            return IntegerResultWithBonus.Failure("GetWeaponDamageModifier failed: " + damageModifiers.ErrorMessage);
        }

        var calculateDamageResult = new CalculateDamage(Target, damageRollWithoutModifiers + damageModifiers.Value, AttackAction.DamageType).Execute();

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
            var applyDamage = new ApplyDamage(EventListener, Target, damage, AttackAction.DamageType);
            return applyDamage.Execute();
        }
    }
}
