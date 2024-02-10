namespace Dnd.Entities.Races.Predefined;

using Dnd.Entities.Traits;

public class HalfElf : IRace
{
    public static readonly HalfElf Instance = new HalfElf();

    private HalfElf()
    {
    }

    public string Name => "Half-Elf";

    public string Description => "Half-elves combine what some say are the best qualities of their elf and human parents.";

    public ICreatureType CreatureType => Races.CreatureType.Humanoid;

    public ISize Size => Races.Size.Medium;

    public List<ATrait> RaceTraits { get; } = new List<ATrait>();
}
