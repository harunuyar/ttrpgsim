namespace DnD.CommandSystem.Commands;

using DnD.CommandSystem.Results;
using DnD.Entities;
using DnD.Entities.Characters;
using DnD.GameManagers.Dice;

internal abstract class DndRollCommand : DndCommand
{
    public DndRollCommand(Character character, EDiceType diceType = EDiceType.D20, EAdvantage? overrideAdvantage = null) : base(character)
    {
        OverrideAdvantage = overrideAdvantage;
        DiceType = diceType;
        IntegerBonuses = new IntegerBonuses(this);
    }

    public EAdvantage? OverrideAdvantage { get; }

    public EDiceType DiceType { get; }

    public IntegerBonuses IntegerBonuses { get; }

    public int? FixedRollResult { get; set; }

    public override IntegerResultWithBonuses Execute()
    {
        if (FixedRollResult.HasValue)
        {
            return IntegerResultWithBonuses.Success(this, "Fixed Roll", FixedRollResult.Value, IntegerBonuses);
        }

        EAdvantage advantage = OverrideAdvantage ?? IntegerBonuses.Advantage;

        string source = "Roll " + DiceType.ToString();
        int rollResult = DiceManager.RollDice(EDiceType.D20);

        if (advantage.HasAdvantage())
        {
            int secondRollResult = DiceManager.RollDice(EDiceType.D20);
            source = $"Roll {DiceType} with advantage, MAX({rollResult}, {secondRollResult})";
            rollResult = Math.Max(rollResult, secondRollResult);
        }
        else if (advantage.HasDisadvantage())
        {
            int secondRollResult = DiceManager.RollDice(EDiceType.D20);
            source = $"Roll {DiceType} with disadvantage, MIN({rollResult}, {secondRollResult})";
            rollResult = Math.Min(rollResult, secondRollResult);
        }

        return IntegerResultWithBonuses.Success(this, source, rollResult, IntegerBonuses);
    }

    public override EventResult IsValid() => EventResult.Success(this);
}
