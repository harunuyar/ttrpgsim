namespace Dnd.GameManagers.Dice;

public class DiceRoll
{
    public DiceRoll(int numberOfDice, EDiceType diceType)
    {
        DiceType = diceType;
        NumberOfDice = numberOfDice;
    }

    EDiceType DiceType { get; set; }

    int NumberOfDice { get; set; }

    public override string ToString()
    {
        return $"{NumberOfDice}{DiceType}";
    }
}
