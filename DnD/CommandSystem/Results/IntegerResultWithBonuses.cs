namespace DnD.CommandSystem.Results;

using TableTopRpg.Commands;

internal class IntegerResultWithBonuses : IntegerResult
{
    public static IntegerResultWithBonuses Success(ICommand command, string source, int baseValue, IntegerBonuses bonuses) => new IntegerResultWithBonuses(command, true, source, baseValue, bonuses);
    public static IntegerResultWithBonuses Failure(ICommand command, string errorMessage) => new IntegerResultWithBonuses(command, false, string.Empty, 0, new IntegerBonuses(command), errorMessage);

    protected IntegerResultWithBonuses(ICommand command, bool success, string source, int baseValue, IntegerBonuses integerBonuses, string? errorMessage = null) : base(command, 0, success, errorMessage)
    {
        IntegerBonuses = integerBonuses;
        Source = source;
        BaseValue = baseValue;
    }

    public IntegerBonuses IntegerBonuses { get; }

    public override int Value => BaseValue + IntegerBonuses.Value;

    public int BaseValue { get; set; }

    public string Source { get; set; }

    public override string ToString()
    {
        if (IsSuccess)
        {
            return $"{Source}: {Value}" + Environment.NewLine + IntegerBonuses.ToString();
        }

        return ErrorMessage ?? "Unknown error";
    }
}
