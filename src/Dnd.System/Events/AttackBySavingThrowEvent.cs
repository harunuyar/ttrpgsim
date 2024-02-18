namespace Dnd.System.Events;

using Dnd.System.CommandSystem.Commands.BooleanResultCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands.Modifiers;
using Dnd.System.CommandSystem.Commands.RollCommands;
using Dnd.System.CommandSystem.Commands.ValueCommands;
using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities.Actions;
using Dnd.System.Entities.Actions.BaseActions;
using Dnd.System.Entities.Damage;
using Dnd.System.Entities.DiceModifiers;
using Dnd.System.Entities.GameActors;
using Dnd.System.Events.EventListener;
using Dnd.System.GameManagers;

public class AttackBySavingThrowEvent : AAttackEvent
{
    private BooleanResult? valid;
    private IntegerResultWithBonus? savingDC, savingThrowModifiers;

    public AttackBySavingThrowEvent(IEventListener eventListener, IAttackAction attackAction, IGameActor attacker, IGameActor target) 
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

    public IntegerResultWithBonus GetSavingDC()
    {
        if (savingDC != null)
        {
            return savingDC;
        }

        if (AttackAction.SuccessMeasuringType != ESuccessMeasuringType.SavingThrow)
        {
            return IntegerResultWithBonus.Failure("AttackAction.SuccessMeasuringType is not SavingThrow");
        }

        var savingDcResult = new GetSavingDC(Attacker, AttackAction).Execute();

        if (!savingDcResult.IsSuccess)
        {
            return IntegerResultWithBonus.Failure("GetSavingDC failed: " + savingDcResult.ErrorMessage);
        }

        this.savingDC = savingDcResult;
        return savingDC;
    }

    public IntegerResultWithBonus GetSavingThrowModifiers()
    {
        if (savingThrowModifiers != null)
        {
            return savingThrowModifiers;
        }

        if (AttackAction.SuccessMeasuringType != ESuccessMeasuringType.SavingThrow)
        {
            return IntegerResultWithBonus.Failure("AttackAction.SuccessMeasuringType is not SavingThrow");
        }

        var modifiers = new GetAttackSavingThrowModifier(Attacker, Target, AttackAction).Execute();

        if (!modifiers.IsSuccess)
        {
            return IntegerResultWithBonus.Failure("GetSavingThrowModifier failed: " + modifiers.ErrorMessage);
        }

        savingThrowModifiers = modifiers;
        return savingThrowModifiers;
    }

    public override int GetSuccessPercentage()
    {
        var savingThrowModifiers = GetSavingThrowModifiers();
        if (!savingThrowModifiers.IsSuccess)
        {
            return 0;
        }

        if (savingThrowModifiers.RollResult.IsSuccess())
        {
            return 100;
        }

        if (savingThrowModifiers.RollResult.IsFailure())
        {
            return 0;
        }

        var savingDC = GetSavingDC();
        if (!savingDC.IsSuccess)
        {
            return 0;
        }

        return Probability.ForSavingThrow(savingDC.Value, savingThrowModifiers.Value, savingThrowModifiers.Advantage).ToPercentage();
    }

    public RollResult RollSavingThrow()
    {
        IntegerResultWithBonus savingThrowModifiers = GetSavingThrowModifiers();

        if (!savingThrowModifiers.IsSuccess)
        {
            return RollResult.Failure("GetAttackRollModifiers failed: " + savingThrowModifiers.ErrorMessage);
        }

        var rollAttack = new RollAttack(EventListener, Attacker, savingThrowModifiers.BonusCollection.Advantage);
        return rollAttack.Execute();
    }

    public ValueResult<ERollResult> GetRollResult(int roll)
    {
        var savingDC = GetSavingDC();

        if (!savingDC.IsSuccess)
        {
            return ValueResult<ERollResult>.Failure("GetSavingDC failed: " + savingDC.ErrorMessage);
        }

        var savingThrowModifiers = GetSavingThrowModifiers();

        if (!savingThrowModifiers.IsSuccess)
        {
            return ValueResult<ERollResult>.Failure("GetSavingThrowModifiers failed: " + savingThrowModifiers.ErrorMessage);
        }

        var rollSuccess = new GetRollSuccess(Attacker, savingDC.Value, roll, savingThrowModifiers.Value).Execute();

        return rollSuccess;
    }

    public RollResult RollDamageDice()
    {
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
        
        var rollAttack = new RollDamage(EventListener, Attacker, damageModifiers.Advantage, AttackAction.DamageDie, false);
        var rollResult = rollAttack.Execute();

        if (!rollResult.IsSuccess)
        {
            return RollResult.Failure("RollDamage failed: " + rollResult.ErrorMessage);
        }

        return rollResult;
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

        var savingThrowModifiers = GetSavingThrowModifiers();

        if (!savingThrowModifiers.IsSuccess)
        {
            return EventResult.Failure("GetSavingThrowModifiers failed: " + savingThrowModifiers.ErrorMessage);
        }

        ERollResult rollResult = savingThrowModifiers.RollResult;

        if (!rollResult.IsSuccess())
        {
            var savingThrow = RollSavingThrow();
            if (!savingThrow.IsSuccess)
            {
                return EventResult.Failure("RollSavingThrow failed: " + savingThrow.ErrorMessage);
            }

            var rollCommandResult = GetRollResult(savingThrow.Value);

            if (!rollCommandResult.IsSuccess)
            {
                return EventResult.Failure("GetRollResult failed: " + rollCommandResult.ErrorMessage);
            }

            rollResult = rollCommandResult.Value;
        }

        if (rollResult.IsSuccess() && AttackAction.FailureDamageModifier == null)
        {
            return EventResult.Success("Saving throw succeeded");
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

        if (rollResult.IsSuccess())
        {
            int savedDamage = AttackAction.FailureDamageModifier!(totalDamage.Value);
            totalDamage.BonusCollection.AddBonus("Saved", totalDamage.Value - savedDamage);
        }

        return ApplyDamage(totalDamage.Value);
    }
}
