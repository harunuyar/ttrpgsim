namespace Dnd.System.Entities.Actions;

public interface IAction : IBonusProvider
{
    string Description { get; }

    EActionType ActionType { get; }

    List<UsageLimitation> UsageLimitations { get; }

    Range Range { get; }

    bool IsAvailable { get; }
}
