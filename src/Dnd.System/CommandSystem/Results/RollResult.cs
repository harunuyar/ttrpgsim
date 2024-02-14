namespace Dnd.System.CommandSystem.Results;

using Dnd.System.Entities.Advantage;

public class RollResult : ICommandResult
{
    public static RollResult Empty() => new(new List<int[]>(), EAdvantage.None, false, null);

    public static RollResult Failure(string errorMessage) => new(new List<int[]>(), EAdvantage.None, false, errorMessage);

    public static RollResult Success(int[] firstRoll) => new(new List<int[]>() { firstRoll }, EAdvantage.None, true, null);

    public static RollResult SuccessAdvantage(int[] firstRoll, int[] secondRoll) => new(new List<int[]>() { firstRoll, secondRoll }, EAdvantage.Advantage, true, null);

    public static RollResult SuccessDisadvantage(int[] firstRoll, int[] secondRoll) => new(new List<int[]>() { firstRoll, secondRoll }, EAdvantage.Disadvantage, true, null);

    private RollResult(List<int[]> rolls, EAdvantage advantage, bool isSuccess, string? errorMessage)
    {
        Rolls = rolls;
        Advantage = advantage;
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
    }

    public int Value => Advantage.HasAdvantage() ? Rolls.Take(2).Max(x => x.Sum()) : (Advantage.HasDisadvantage() ? Rolls.Take(2).Min(x => x.Sum()) : Rolls.FirstOrDefault(new int[0]).Sum());

    public List<int[]> Rolls { get; private set; }

    public EAdvantage Advantage { get; set; }

    public bool IsSuccess { get; private set; }

    public string? ErrorMessage { get; private set; }

    public void AddAdvantageRoll(EAdvantage advantage, int[] roll)
    {
        Advantage |= advantage;
        Rolls.Add(roll);
    }

    public void AddRoll(int[] roll)
    {
        Rolls.Add(roll);
    }

    public void Set(EAdvantage advantage, List<int[]> rolls)
    {
        Rolls = rolls;
        Advantage = advantage;
    }

    public void SetError(string errorMessage)
    {
        IsSuccess = false;
        ErrorMessage = errorMessage;
    }

    public void Reset()
    {
        Rolls.Clear();
        Advantage = EAdvantage.None;
        IsSuccess = true;
        ErrorMessage = null;
    }
}
