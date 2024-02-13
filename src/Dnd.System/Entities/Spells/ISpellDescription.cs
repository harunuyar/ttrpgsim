namespace Dnd.System.Entities.Spells;

using Dnd.System.Entities.Units;

public interface ISpellDescription : IDndEntity
{
    string Description { get; }

    int Level { get; }

    TimeSpan CastingTime { get; }

    ESuccessMeasuringType SuccessMeasuringType { get; }
}
