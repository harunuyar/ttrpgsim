namespace Dnd.System.Events;

using Dnd.System.CommandSystem.Commands.BooleanResultCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands.Modifiers;
using Dnd.System.CommandSystem.Commands.RollCommands;
using Dnd.System.CommandSystem.Commands.ValueCommands;
using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities.Actions;
using Dnd.System.Entities.Actions.BaseActions;
using Dnd.System.Entities.DiceModifiers;
using Dnd.System.Entities.GameActors;
using Dnd.System.Events.EventListener;
using Dnd.System.GameManagers;

public class AttackByAttackRollEvent : AAttackEvent
{
    private BooleanResult? valid;
    private IntegerResultWithBonus? attackRollModifiers, armorClass;

    public AttackByAttackRollEvent(IEventListener eventListener, IAttackAction attackAction, IGameActor attacker, IGameActor target) 
        : base(eventListener, attackAction, attacker, target)
    {
    }

    public override BooleanResult IsValid()
    {
        if (valid != null)
        {
            return valid;
        }

        var canAttack = new CanAttackTarget(Attacker, Target).Execute();

        if (!canAttack.IsSuccess)
        {
            return BooleanResult.Failure("CanAttackTarget failed: " + canAttack.ErrorMessage);
        }

        if (!canAttack.Value)
        {
            return canAttack;
        }

        valid = canAttack;
        return valid;
    }

    public IntegerResultWithBonus GetAttackRollModifiers()
    {
        if (attackRollModifiers != null)
        {
            return attackRollModifiers;
        }

        if (AttackAction.SuccessMeasuringType != ESuccessMeasuringType.AttackRoll)
        {
            return IntegerResultWithBonus.Failure("AttackAction.SuccessMeasuringType is not AttackRoll");
        }

        var attackModifiers = new GetAttackModifier(Attacker, Target, AttackAction).Execute();

        if (!attackModifiers.IsSuccess)
        {
            return IntegerResultWithBonus.Failure("GetAttackModifier failed: " + attackModifiers.ErrorMessage);
        }

        attackRollModifiers = attackModifiers;
        return attackRollModifiers;
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

    public override int GetSuccessPercentage()
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

        if (!attackRollModifiers.IsSuccess)
        {
            return RollResult.Failure("GetAttackRollModifiers failed: " + attackRollModifiers.ErrorMessage);
        }

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
            return EventResult.Success("Attack missed due to critical failure.");
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

        if (attackRollModifiers.HitResult == EHitResult.CriticalHit)
        {
            rollResult = ERollResult.CriticalSuccess;
        }

        if (rollResult.IsFailure())
        {
            return EventResult.Success("Attack missed.");
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
