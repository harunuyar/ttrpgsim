namespace Dnd.Predefined.Races;

using Dnd.System.Entities.Races;
using Dnd.System.Entities.Traits;
using Dnd.Predefined.CreatureTypes;
using Dnd.Predefined.Sizes;

public class Gnome : IRace
{
    public static readonly Gnome Instance = new Gnome();

    private Gnome()
    {
    }

    public string Name => "Gnome";

    public string Description => "A gnome's energy and enthusiasm for living shines through every inch of his or her tiny body.";

    public ICreatureType CreatureType => Humanoid.Instance;

    public ISize Size => Small.Instance;

    public List<ITrait> RaceTraits { get; } = new List<ITrait>();
}
