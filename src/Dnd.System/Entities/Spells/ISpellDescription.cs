namespace Dnd.System.Entities.Spells;

public interface ISpellDescription : IDndEntity
{
    string Description { get; }

    ESuccessMeasuringType SuccessMeasuringType { get; }
}
