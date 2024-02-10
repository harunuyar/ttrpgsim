namespace DnD.GameManagers.Dice;

public class DiceManager
{
    public static int RollDice(EDiceType diceType)
    {
        return new Random().Next(1, (int)diceType + 1);
    }
}
