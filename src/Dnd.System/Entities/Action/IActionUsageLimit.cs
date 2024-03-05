namespace Dnd.System.Entities.Action;

[Flags]
public enum EActionUsageLimitType
{
    None = 0,
    PerTurn = 1,
    PerRound = 2,
    PerCombat = 4,
    PerShortRest = 8,
    PerLongRest = 16
}

public interface IActionUsageLimit : ICommandHandler
{
    EActionUsageLimitType Type { get; }
    int Limit { get; }
    int Current { get; }
    bool IsExhausted => Type == EActionUsageLimitType.None || Current >= Limit;
    void Use();
}
