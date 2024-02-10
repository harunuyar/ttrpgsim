namespace Dnd.Entities.Races.Predefined;

using Dnd.Entities.Traits;

public class Halfling : IRace
{
    public static readonly Halfling Instance = new Halfling();

    private Halfling()
    {
    }

    public string Name => "Halfling";

    public string Description => "The diminutive halflings survive in a world full of larger creatures by avoiding notice or, barring that, avoiding offense.";

    public ICreatureType CreatureType => Races.CreatureType.Humanoid;

    public ISize Size => Races.Size.Small;

    public List<ATrait> RaceTraits { get; } = new List<ATrait>();
}
