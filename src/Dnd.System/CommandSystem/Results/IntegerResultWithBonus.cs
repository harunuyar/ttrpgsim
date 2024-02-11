namespace Dnd.System.CommandSystem.Results;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities;

public class IntegerResultWithBonus : ICommandResult
{
    public static IntegerResultWithBonus Empty(ICommand command) => new IntegerResultWithBonus(command, true, null, null, 0, new BonusCollection());

    public static IntegerResultWithBonus Success(ICommand command, IDndEntity source, int baseValue, BonusCollection bonuses) => new IntegerResultWithBonus(command, true, null, source, baseValue, bonuses);

    public static IntegerResultWithBonus Failure(ICommand command, string errorMessage) => new IntegerResultWithBonus(command, false, errorMessage, null, 0, new BonusCollection());

    private IntegerResultWithBonus(ICommand command, bool success, string? errorMessage, IDndEntity? source, int baseValue, BonusCollection bonusCollection)
    {
        Command = command;
        IsSuccess = success;
        ErrorMessage = errorMessage;
        Source = source;
        BaseValue = baseValue;
        BonusCollection = bonusCollection;
    }

    public BonusCollection BonusCollection { get; }

    public int Value => BaseValue + BonusCollection.TotalValue;

    public int BaseValue { get; private set; }

    public IDndEntity? Source { get; private set; }

    public bool IsSuccess { get; private set; }

    public string? ErrorMessage { get; private set; }

    public ICommand Command { get; }

    public void SetError(string errorMessage)
    {
        IsSuccess = false;
        ErrorMessage = errorMessage;
    }

    public void SetBaseValue(IDndEntity source, int value)
    {
        Source = source;
        BaseValue = value;
    }

    public void SetBaseValue(string source, int value)
    {
        SetBaseValue(new CustomDndEntity(source), value);
    }

    public void Reset()
    {
        IsSuccess = true;
        ErrorMessage = null;
        Source = null;
        BaseValue = 0;
        BonusCollection.Reset();
    }

    public override string ToString()
    {
        return IsSuccess ? $"{Source}: {Value}" + Environment.NewLine + BonusCollection.ToString() : ErrorMessage ?? "Unknown error";
    }
}
