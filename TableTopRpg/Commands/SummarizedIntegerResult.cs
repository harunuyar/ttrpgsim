namespace TableTopRpg.Commands;

public class SummarizedIntegerResult : IntegerResult
{
    public static SummarizedIntegerResult Success(IntegerResult integerResult) => new SummarizedIntegerResult(integerResult.Command, integerResult.IsSuccess, integerResult.Message, integerResult.Value);
    public static SummarizedIntegerResult Success(ICommand command) => new SummarizedIntegerResult(command, true, null, null);
    new public static SummarizedIntegerResult Success(ICommand command, int value, string message) => new SummarizedIntegerResult(command, true, message, value);
    new public static SummarizedIntegerResult Failure(ICommand command, string? message) => new SummarizedIntegerResult(command, false, message, 0);

    protected SummarizedIntegerResult(ICommand command, bool success, string? message, int? baseValue) : base(command, success, message, 0)
    {
        BaseValue = baseValue;
        BaseMessage = message;
        BonusValues = new List<(int value, string message)>();
    }

    public int? BaseValue { get; set; }

    public string? BaseMessage { get; set; }

    public List<(int Value, string Message)> BonusValues { get; }

    new public int Value => BaseValue ?? 0 + BonusValues.Sum(x => x.Value);

    new public string Message => (BaseMessage == null || BaseValue == null ? "No Base Value" : FormatMessage(BaseMessage, BaseValue.Value, true)) + Environment.NewLine
        + string.Join(Environment.NewLine, BonusValues.Select(x => FormatMessage(x.Message, x.Value, false)));

    private static string FormatMessage(string message, int value, bool baseValue) => $"{message}: {(value >= 0 ? "+" : "")}{value}";
}
