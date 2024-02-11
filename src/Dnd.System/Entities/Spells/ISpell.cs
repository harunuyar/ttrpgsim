namespace Dnd.System.Entities.Spells;

public interface ISpell : IDndEntity
{
    string Description { get; }

    ESuccessMeasuringType SuccessMeasuringType { get; }
}
