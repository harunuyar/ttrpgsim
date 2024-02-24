namespace Dnd.System.GameManagers.Dice;

public static class Probability
{
    public static int ToPercentage(this double value)
    {
        return (int)Math.Round(value * 100);
    }

    public static double CustomRoll(DiceRoll diceRoll, EAdvantage advantage, int target)
    {
        double result = CalculateSuccessPercentage(diceRoll.NumberOfDice, (int)diceRoll.DiceType, target);

        if (advantage == EAdvantage.Advantage)
        {
            result = 1 - (1 - result) * (1 - result);
        }
        else if (advantage == EAdvantage.Disadvantage)
        {
            result = 1 - result * result;
        }

        return result;
    }

    public static double ForSavingThrow(int savingDC, int modifiers, EAdvantage advantage)
    {
        return ForSavingThrow(savingDC - modifiers, advantage);
    }

    public static double ForAttackRoll(int modifiers, EAdvantage advantage, int target)
    {
        return ForAttackRoll(advantage, target - modifiers);
    }

    public static double ForSavingThrow(int savingDC, EAdvantage advantage)
    {
        return ForAttackRoll(advantage, savingDC);
    }

    public static double ForAttackRoll(EAdvantage advantage, int target)
    {
        double result = ForAttackRoll(target);

        if (advantage == EAdvantage.Advantage)
        {
            result = 1 - (1 - result) * (1 - result);
        }
        else if (advantage == EAdvantage.Disadvantage)
        {
            result = 1 - result * result;
        }

        return result;
    }

    private static double ForAttackRoll(int target)
    {
        return CalculateSuccessPercentage(1, 20, target);
    }

    public static double CalculateSuccessPercentage(int numDice, int faceNumber, int targetValue)
    {
        double[,] dp = new double[numDice + 1, targetValue + 1];

        // Initialize memoization array
        for (int i = 0; i <= numDice; i++)
        {
            for (int j = 0; j <= targetValue; j++)
            {
                dp[i, j] = -1.0;
            }
        }

        return CalculateSuccessProbability(dp, numDice, faceNumber, targetValue);
    }

    private static double CalculateSuccessProbability(double[,] dp, int numDice, int faceNumber, int targetValue)
    {
        if (targetValue <= 0)
            return 1.0; // Base case: If targetValue is non-positive, success probability is 1.0
        if (numDice <= 0)
            return 0.0; // Base case: If no dice left, success probability is 0.0

        if (dp[numDice, targetValue] != -1.0)
            return dp[numDice, targetValue];

        double probability = 0.0;

        // Recursively calculate success probability for each possible dice roll
        for (int i = 1; i <= faceNumber; i++)
        {
            probability += 1.0 / faceNumber * CalculateSuccessProbability(dp, numDice - 1, faceNumber, targetValue - i);
        }

        dp[numDice, targetValue] = probability; // Memoize the result

        return probability;
    }
}