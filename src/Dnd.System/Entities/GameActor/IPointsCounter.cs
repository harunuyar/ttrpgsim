namespace Dnd.System.Entities.GameActor;

public interface IPointsCounter : ICommandHandler
{
    int ActionPoints { get; }
    int BonusActionPoints { get; }
    int ReactionPoints { get; }
    void Reset();
    void AddExtraActionPoint();
    void UseActionPoint();
    void UseBonusActionPoint();
    void UseReactionPoint();
    int GetUsedSpellCounts(int spellLevel);
    void UseSpellSlot(int spellLevel);
    void ResetSpellSlots();
}
