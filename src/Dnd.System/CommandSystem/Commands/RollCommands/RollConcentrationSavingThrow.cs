namespace Dnd.System.CommandSystem.Commands.RollCommands;

using Dnd.GameManagers.Dice;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.Characters;
using Dnd.System.Entities.Items.Equipments.Weapons;

public class RollConcentrationSavingThrow : DndRollCommand
{
    public RollConcentrationSavingThrow(IEventListener eventListener, ICharacter character, int damageTaken, EDamageType damageType) : base(eventListener, character, EDiceType.d20)
    {
        DamageTaken = damageTaken;
        DamageType = damageType;
    }

    public int DamageTaken { get; }

    public EDamageType DamageType { get; }

    public override void FinalizeEvent()
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
            EventResult.SetError($"{Character.Name} could not roll a concentration check. CalculateConcentrationSavingDifficultyClass: " 
                + (savingDCResult.ErrorMessage ?? "CalculateConcentrationSavingDifficultyClass Error"));
            return;
        }

        var getSavingThrowModifier = new GetSavingThrowModifier(Character, DamageType, EAttributeType.Constitution);
        var savingThrowModifierResult = getSavingThrowModifier.Execute();

        if (!savingThrowModifierResult.IsSuccess)
        {
            EventResult.SetError($"{Character.Name} could not roll a concentration check. GetSavingThrowModifier: "
                + (savingThrowModifierResult.ErrorMessage ?? "GetSavingThrowModifier Error"));
            return;
        }

        var rollResult = RollDice();

        if (rollResult + savingThrowModifierResult.Value < savingDCResult.Value)
        {
            EventResult.SetMessage($"{Character.Name} failed their concentration check and lost concentration. Rolled: {rollResult} + Constitution saving throw modifier: {savingThrowModifierResult.Value} against {savingDCResult.Value} DC");
            Character.EffectsTable.RemoveConcentration();
        }
        else
        {
            EventResult.SetMessage($"{Character.Name} succeeded their concentration check and maintained concentration. Rolled: {rollResult} + Constitution saving throw modifier: {savingThrowModifierResult.Value} against {savingDCResult.Value} DC");
        }
    }
}
