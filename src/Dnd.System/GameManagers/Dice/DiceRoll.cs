namespace Dnd.System.GameManagers.Dice;

using global::System.Text.RegularExpressions;

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

    public int MaxRoll()
    {
        return NumberOfDice * (int)DiceType;
    }

    public int MinRoll()
    {
        return NumberOfDice;
    }

    public static DiceRoll Parse(string input)
    {
        var parts = input.ToLower().Split('d');
        int count = 1;
        if (parts.Length > 0)
        {
            if (!string.IsNullOrWhiteSpace(parts[0]))
                count = int.Parse(parts[0]);
        }

        var sides = (EDiceType)int.Parse(parts[1]);

        return new DiceRoll(count, sides);
    }
}
