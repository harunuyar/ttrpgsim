namespace Dnd.GameManagers.Dice;

public class DiceRoll
{
    public DiceRoll(int numberOfDice, EDiceType diceType)
    {
        DiceType = diceType;
        NumberOfDice = numberOfDice;
    }

    public EDiceType DiceType { get; set; }

    public int NumberOfDice { get; set; }

    public override string ToString()
    {
        return $"{NumberOfDice}{DiceType}";
    }

    public int[] Roll()
    {
        int[] rolls = new int[NumberOfDice];

        for (int i = 0; i < NumberOfDice; i++)
        {
            rolls[i] = DiceManager.RollDice(DiceType);
        }

        return rolls;
    }

    public int[] MaxRoll()
    {
        int[] rolls = new int[NumberOfDice];

        for (int i = 0; i < NumberOfDice; i++)
        {
            rolls[i] = (int)DiceType;
        }

        return rolls;
    }

    public int[] MinRoll()
    {
        int[] rolls = new int[NumberOfDice];

        for (int i = 0; i < NumberOfDice; i++)
        {
            rolls[i] = 1;
        }

        return rolls;
    }
}
