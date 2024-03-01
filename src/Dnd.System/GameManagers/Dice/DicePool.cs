namespace Dnd.System.GameManagers.Dice;

using global::System.Text.RegularExpressions;

public partial class DicePool
{
    public DicePool(IEnumerable<DiceRoll> positiveDiceRolls, int bonus)
    {
        Rolls = positiveDiceRolls.ToList();
        Bonus = bonus;
    }

    public List<DiceRoll> Rolls { get; set; }

    public int Bonus { get; set; }

    public int MaxRoll()
    {
        return (Rolls.Count != 0 ? Rolls.Sum(r => r.MaxRoll()) : 0) + Bonus;
    }

    public int MinRoll()
    {
        return (Rolls.Count != 0 ? Rolls.Sum(r => r.MinRoll()) : 0) + Bonus;
    }

    public static DicePool Parse(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return new DicePool(new List<DiceRoll>(), 0);
        }

        var diceRolls = new List<DiceRoll>();
        int bonus = 0;

        input = WhiteSpaceRegex().Replace(input, "").ToLower();

        // Define regex pattern to match dice rolls and constant bonuses
        var matches = DicePoolRegex().Matches(input);

        foreach (Match match in matches)
        {
            var token = match.Value.Trim();
            if (token.StartsWith('d') || token.Contains('d'))
            {
                // If the token contains 'd', it's a dice roll
                var diceRoll = DiceRoll.Parse(token);
                diceRoll.Negative = token.StartsWith('-');
                diceRolls.Add(diceRoll);
            }
            else
            {
                // Otherwise, it's a constant bonus
                int constantBonus = int.Parse(token);
                if (token.StartsWith('-'))
                {
                    bonus -= constantBonus;
                }
                else
                {
                    bonus += constantBonus;
                }
            }
        }

        return new DicePool(diceRolls, bonus);
    }

    [GeneratedRegex(@"\s+")]
    private static partial Regex WhiteSpaceRegex();

    [GeneratedRegex(@"([+-]?\d+d\d+)|([+-]?\d+)")]
    private static partial Regex DicePoolRegex();
}
