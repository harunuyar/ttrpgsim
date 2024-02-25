namespace Dnd.System.GameManagers.Dice;

using global::System.Text.RegularExpressions;

public partial class DicePool
{
    public DicePool(IEnumerable<DiceRoll> positiveDiceRolls, IEnumerable<DiceRoll> negativeDiceRolls, int bonus)
    {
        PositiveRolls = positiveDiceRolls.ToList();
        NegativeRolls = negativeDiceRolls.ToList();
        Bonus = bonus;
    }

    public List<DiceRoll> PositiveRolls { get; set; }

    public List<DiceRoll> NegativeRolls { get; set; }

    public int Bonus { get; set; }

    public int MaxRoll()
    {
        return (PositiveRolls.Count != 0 ? PositiveRolls.Sum(r => r.MaxRoll()) : 0) 
            - (NegativeRolls.Count != 0 ? NegativeRolls.Sum(r => r.MinRoll()) : 0) 
            + Bonus;
    }

    public int MinRoll()
    {
        return (PositiveRolls.Count != 0 ? PositiveRolls.Sum(r => r.MinRoll()) : 0)
            - (NegativeRolls.Count != 0 ? NegativeRolls.Sum(r => r.MaxRoll()) : 0)
            + Bonus;
    }

    public static DicePool Parse(string input)
    {
        var positiveDiceRolls = new List<DiceRoll>();
        var negativeDiceRolls = new List<DiceRoll>();
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
                if (token.StartsWith('-'))
                {
                    negativeDiceRolls.Add(diceRoll);
                }
                else
                {
                    positiveDiceRolls.Add(diceRoll);
                }
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

        return new DicePool(positiveDiceRolls, negativeDiceRolls, bonus);
    }

    [GeneratedRegex(@"\s+")]
    private static partial Regex WhiteSpaceRegex();

    [GeneratedRegex(@"([+-]?\d+d\d+)|([+-]?\d+)")]
    private static partial Regex DicePoolRegex();
}
