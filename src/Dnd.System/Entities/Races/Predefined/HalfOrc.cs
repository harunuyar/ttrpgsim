namespace Dnd.Entities.Races.Predefined;

using Dnd.Entities.Traits;

public class HalfOrc : IRace
{
    public static readonly HalfOrc Instance = new HalfOrc();

    private HalfOrc()
    {
    }

    public string Name => "Half-Orc";

    public string Description => "Half-orcs are monstrosities, their tragic births the result of perversion and violence—or at least, that's how they're viewed by many.";

    public ICreatureType CreatureType => Races.CreatureType.Humanoid;

    public ISize Size => Races.Size.Medium;

    public List<ATrait> RaceTraits { get; } = new List<ATrait>();
}
