namespace Dnd.Predefined.Races;

using Dnd.System.Entities.Races;
using Dnd.System.Entities.Traits;
using Dnd.Predefined.CreatureTypes;
using Dnd.Predefined.Sizes;

public class Halfling : IRace
{
    public static readonly Halfling Instance = new Halfling();

    private Halfling()
    {
    }

    public string Name => "Halfling";

    public string Description => "The diminutive halflings survive in a world full of larger creatures by avoiding notice or, barring that, avoiding offense.";

    public ICreatureType CreatureType => Humanoid.Instance;

    public ISize Size => Small.Instance;

    public List<ITrait> RaceTraits { get; } = new List<ITrait>();
}
