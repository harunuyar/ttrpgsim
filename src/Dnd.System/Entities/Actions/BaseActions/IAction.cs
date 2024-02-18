namespace Dnd.System.Entities.Actions.BaseActions;

using Dnd.System.Entities.GameActors;

public interface IAction : IBonusProvider
{
    string Description { get; }

    EActionType ActionType { get; }

    List<UsageLimitation> UsageLimitations { get; }

    Range Range { get; }

    bool IsAvailable(IGameActor gameActor);

    void Use();

    void Apply(IGameActor actor, IEnumerable<IGameActor> targets);
}
