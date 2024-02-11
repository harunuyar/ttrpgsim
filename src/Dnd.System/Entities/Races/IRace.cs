namespace Dnd.System.Entities.Races;

using Dnd.System.Entities;
using Dnd.System.Entities.Traits;

public interface IRace : IDndEntity
{
    string Description { get; }

    ICreatureType CreatureType { get; }

    ISize Size { get; }

    List<ITrait> RaceTraits { get; }
}
