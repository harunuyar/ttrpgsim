namespace Dnd.System.CommandSystem.Commands.EventCommands;

using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
using Dnd.System.CommandSystem.Commands.RollCommands;
using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Items.Equipments.Weapons;

public class ConcentrationCheckAfterDamage : DndEventCommand
{
    public ConcentrationCheckAfterDamage(IEventListener eventListener, IGameActor character, int damageTaken, EDamageType damageType) : base(eventListener, character)
    {
        DamageTaken = damageTaken;
        DamageType = damageType;
    }

    public int DamageTaken { get; }

    public EDamageType DamageType { get; }

    protected override void InitializeEvent()
    {
    }

    protected override void FinalizeEvent()
    {
        if (Character.EffectsTable.Concentration == null)
        {
            EventResult.SetMessage($"{Character.Name} does not have a concentration effect to break.");
            return;
        }

        var calculateSavingDC = new CalculateConcentrationSavingDifficultyClass(Character, DamageTaken);
        var savingDCResult = calculateSavingDC.Execute();

        if (!savingDCResult.IsSuccess)
        {
            EventResult.SetError($"{Character.Name} could not roll a concentration check. CalculateConcentrationSavingDifficultyClass: " + savingDCResult.ErrorMessage);
            return;
        }

        var getSavingThrowModifier = new GetConcentrationSavingThrowModifier(Character, DamageType);
        var savingThrowModifierResult = getSavingThrowModifier.Execute();

        if (!savingThrowModifierResult.IsSuccess)
        {
            EventResult.SetError($"{Character.Name} could not roll a concentration check. GetConcentrationSavingThrowModifier: " + savingThrowModifierResult.ErrorMessage);
            return;
        }

        var rollConcentrationSavingThrow = new RollConcentrationSavingThrow(EventListener, Character, savingThrowModifierResult.BonusCollection.Advantage);
        var rollResult = rollConcentrationSavingThrow.Execute();

        if (!rollResult.IsSuccess)
        {
            EventResult.SetError($"{Character.Name} could not roll a concentration check. RollConcentrationSavingThrow: " + rollResult.ErrorMessage);
            return;
        }

        if (rollResult.TotalValue + savingThrowModifierResult.Value < savingDCResult.Value)
        {
            EventResult.SetMessage($"{Character.Name} failed their concentration check and lost concentration. Rolled: {rollResult.TotalValue} + Constitution saving throw modifier: {savingThrowModifierResult.Value} against {savingDCResult.Value} DC");
            Character.EffectsTable.RemoveConcentration();
        }
        else
        {
            EventResult.SetMessage($"{Character.Name} succeeded their concentration check and maintained concentration. Rolled: {rollResult.TotalValue} + Constitution saving throw modifier: {savingThrowModifierResult.Value} against {savingDCResult.Value} DC");
        }
    }
}
