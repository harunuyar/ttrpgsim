namespace Dnd.Predefined.Races;

using Dnd.System.Entities.Races;
using Dnd.System.Entities.Traits;
using Dnd.Predefined.CreatureTypes;
using Dnd.Predefined.Sizes;
using Dnd.System.Entities.Units;

public class Tiefling : IRace
{
    public Tiefling()
    {
    }

    public string Name => "Tiefling";

    public string Description => "Tieflings are derived from human bloodlines, and in the broadest possible sense, they still look human";

    public ICreatureType CreatureType => Humanoid.Instance;

    public ISize Size => Medium.Instance;

    public List<ITrait> RaceTraits { get; } = new List<ITrait>();

    public Distance Speed => Distance.OfFeet(30);
}
