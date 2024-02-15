namespace Dnd.Predefined.Races;

using Dnd.System.Entities.Races;
using Dnd.System.Entities.Traits;
using Dnd.Predefined.CreatureTypes;
using Dnd.Predefined.Sizes;
using Dnd.System.Entities.Units;

public class HalfOrc : IRace
{
    public HalfOrc()
    {
    }

    public string Name => "Half-Orc";

    public string Description => "Half-orcs are monstrosities, their tragic births the result of perversion and violence—or at least, that's how they're viewed by many.";

    public ICreatureType CreatureType => Humanoid.Instance;

    public ISize Size => Medium.Instance;

    public List<ITrait> RaceTraits { get; } = new List<ITrait>();

    public Distance Speed => Distance.OfFeet(30);
}
