namespace DnD.Entities.Races;

using DnD.Entities;
using DnD.Entities.Traits;

internal interface IRace : IDndEntity
{
    string Description { get; }
    ICreatureType CreatureType { get; }
    ISize Size { get; }
    List<ATrait> RaceTraits { get; }
}
