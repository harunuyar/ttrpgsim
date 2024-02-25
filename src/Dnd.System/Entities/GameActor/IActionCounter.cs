namespace Dnd.System.Entities.GameActor;

public interface IActionCounter : ICommandHandler
{
    int ActionPoints { get; }
    int BonusActionPoints { get; }
    int ReactionPoints { get; }
    void Reset();
    void AddExtraActionPoint();
    void UseActionPoint();
    void UseBonusActionPoint();
    void UseReactionPoint();
}
