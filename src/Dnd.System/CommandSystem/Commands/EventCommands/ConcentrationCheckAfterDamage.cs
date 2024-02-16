namespace Dnd.System.CommandSystem.Commands.EventCommands;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
using Dnd.System.CommandSystem.Commands.RollCommands;
using Dnd.System.CommandSystem.Commands.ValueCommands;
using Dnd.System.Entities.Advantage;
using Dnd.System.Entities.Damage;
using Dnd.System.Entities.GameActors;
using Dnd.System.Events.EventListener;

public class ConcentrationCheckAfterDamage : DndEventCommand
{
    public ConcentrationCheckAfterDamage(IEventListener eventListener, IGameActor character, int damageTaken, EDamageType damageType) : base(eventListener, character)
    {
        DamageTaken = damageTaken;
        DamageType = damageType;
    }

    public int DamageTaken { get; }

    public EDamageType DamageType { get; }

    protected override void FinalizeResult()
    {
        if (Actor.EffectsTable.Concentration == null)
        {
            Result.SetMessage($"{Actor.Name} does not have a concentration effect to break.");
            return;
        }

        var calculateSavingDC = new GetConcentrationSavingDC(Actor, DamageTaken);
        var savingDCResult = calculateSavingDC.Execute();

        if (!savingDCResult.IsSuccess)
        {
            Result.SetError($"{Actor.Name} could not roll a concentration check. CalculateConcentrationSavingDifficultyClass: " + savingDCResult.ErrorMessage);
            return;
        }

        var getSavingThrowModifier = new GetConcentrationSavingThrowModifier(Actor, DamageType);
        var savingThrowModifierResult = getSavingThrowModifier.Execute();

        if (!savingThrowModifierResult.IsSuccess)
        {
            Result.SetError($"{Actor.Name} could not roll a concentration check. GetConcentrationSavingThrowModifier: " + savingThrowModifierResult.ErrorMessage);
            return;
        }

        if (savingThrowModifierResult.RollResult.IsCriticalSuccess())
        {
            Result.SetMessage($"{Actor.Name} has guaranteed critical success to maintain concentration.");
            return;
        }
        else if (savingThrowModifierResult.RollResult.IsCriticalFailure())
        {
            Result.SetMessage($"{Actor.Name} has guaranteed critical failure to maintain concentration.");
            Actor.EffectsTable.RemoveConcentration();
            return;
        }

        var rollConcentrationSavingThrow = new RollConcentrationSavingThrow(EventListener, Actor, savingThrowModifierResult.BonusCollection.Advantage);
        var rollResult = rollConcentrationSavingThrow.Execute();

        if (!rollResult.IsSuccess)
        {
            Result.SetError($"{Actor.Name} could not roll a concentration check. RollConcentrationSavingThrow: " + rollResult.ErrorMessage);
            return;
        }

        var rollSuccess = new GetRollSuccess(Actor, savingDCResult.Value, rollResult.Value, savingThrowModifierResult.Value).Execute();

        if (!rollSuccess.IsSuccess)
        {
            Result.SetError($"{Actor.Name} could not roll a concentration check. GetRollResult: " + rollResult.ErrorMessage);
            return;
        }

        if (rollSuccess.Value.IsFailure())
        {
            Result.SetMessage($"{Actor.Name} failed their concentration check and lost concentration. Rolled: {rollResult.Value} + Constitution saving throw modifier: {savingThrowModifierResult.Value} against {savingDCResult.Value} DC");
            Actor.EffectsTable.RemoveConcentration();
        }
        else
        {
            Result.SetMessage($"{Actor.Name} succeeded their concentration check and maintained concentration. Rolled: {rollResult.Value} + Constitution saving throw modifier: {savingThrowModifierResult.Value} against {savingDCResult.Value} DC");
        }
    }
}
