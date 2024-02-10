namespace Dnd.Entities.Races.Predefined;

using Dnd.Entities.Traits;

public class Gnome : IRace
{
    public static readonly Gnome Instance = new Gnome();

    private Gnome()
    {
    }

    public string Name => "Gnome";

    public string Description => "A gnome's energy and enthusiasm for living shines through every inch of his or her tiny body.";

    public ICreatureType CreatureType => Races.CreatureType.Humanoid;

    public ISize Size => Races.Size.Small;

    public List<ATrait> RaceTraits { get; } = new List<ATrait>();
}
