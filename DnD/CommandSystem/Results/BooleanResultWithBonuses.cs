namespace DnD.CommandSystem.Results;

using TableTopRpg.Commands;

internal class BooleanResultWithBonuses : BooleanResult
{
    public static BooleanResultWithBonuses Success(ICommand command, string source, bool baseValue, BooleanBonuses? bonuses = null) => new BooleanResultWithBonuses(command, true, source, baseValue, bonuses ?? new BooleanBonuses(command));
    public static BooleanResultWithBonuses Failure(ICommand command, string errorMessage) => new BooleanResultWithBonuses(command, false, string.Empty, false, new BooleanBonuses(command), errorMessage);

    public BooleanResultWithBonuses(ICommand command, bool success, string source, bool baseValue, BooleanBonuses bonuses, string? errorMsg = null) : base(command, false, success, errorMsg)
    {
        BooleanBonuses = bonuses;
        Source = source;
        BaseValue = baseValue;
    }

    public BooleanBonuses BooleanBonuses { get; }

    public string Source { get; }

    public bool BaseValue { get; }

    public override bool Value => BaseValue || BooleanBonuses.Value;

    public override string ToString()
    {
        if (IsSuccess)
        {
            return $"{Source}: {Value}" + Environment.NewLine + BooleanBonuses.ToString();
        }
        
        return ErrorMessage ?? "Unknown error";
    }
}
