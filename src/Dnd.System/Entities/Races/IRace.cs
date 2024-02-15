namespace Dnd.System.Entities.Races;

using Dnd.System.Entities;
using Dnd.System.Entities.Traits;
using Dnd.System.Entities.Units;

public interface IRace : IDndEntity
{
    string Description { get; }

    ICreatureType CreatureType { get; }

    ISize Size { get; }

    Distance Speed { get; }

    List<ITrait> RaceTraits { get; }
}
