namespace Dnd.System.Entities.Spells;

public interface ISpell : IDndEntity
{
    string Description { get; }

    int Level { get; }

    TimeSpan CastingTime { get; }

    ESuccessMeasuringType SuccessMeasuringType { get; }
}
