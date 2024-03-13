namespace Dnd.System.GameManagers.Dice;

public class DiceRollResult
{
    public DiceRollResult(EDiceType diceType, EAdvantage advantage, bool negative)
    {
        DiceType = diceType;
        Advantage = advantage;
        Results = [];
        Roll();
    }

    public EDiceType DiceType { get; }

    public EAdvantage Advantage { get; }

    public bool Negative { get; }

    public int[] Results { get; private set; }

    public int Result => (Negative ? -1 : 1) * (Advantage.HasAdvantage() ? (Negative ? Results.Min() : Results.Max()) : (Negative ? Results.Max() : Results.Min()));

    public void Roll()
    {
        if (Advantage == EAdvantage.None)
        {
            Results = [DiceManager.RollDice(DiceType)];
        }
        else
        {
            Results = [DiceManager.RollDice(DiceType), DiceManager.RollDice(DiceType)];
        }
    }
}
