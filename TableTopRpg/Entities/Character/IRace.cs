namespace TableTopRpg.Entities.Character;

public interface IRace
{
    string Name { get; }
    string Description { get; }
    ICreatureType CreatureType { get; }
    ISize Size { get; }
    List<ITrait> RaceTraits { get; }
}
