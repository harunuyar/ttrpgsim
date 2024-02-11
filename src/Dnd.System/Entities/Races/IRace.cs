namespace Dnd.Entities.Races;

using Dnd.Entities;
using Dnd.Entities.Traits;

public interface IRace : IDndEntity
{
    string Description { get; }
    ICreatureType CreatureType { get; }
    ISize Size { get; }
    List<ATrait> RaceTraits { get; }
}
