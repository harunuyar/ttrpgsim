namespace Dnd.Predefined.Races;

using Dnd.System.Entities.Races;
using Dnd.System.Entities.Traits;
using Dnd.Predefined.CreatureTypes;
using Dnd.Predefined.Sizes;

public class Tiefling : IRace
{
    public static readonly Tiefling Instance = new Tiefling();

    private Tiefling()
    {
    }

    public string Name => "Tiefling";

    public string Description => "Tieflings are derived from human bloodlines, and in the broadest possible sense, they still look human";

    public ICreatureType CreatureType => Humanoid.Instance;

    public ISize Size => Medium.Instance;

    public List<ITrait> RaceTraits { get; } = new List<ITrait>();
}
