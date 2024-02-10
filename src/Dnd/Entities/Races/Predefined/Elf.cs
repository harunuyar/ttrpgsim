namespace Dnd.Entities.Races.Predefined;

using Dnd.Entities.Traits;

public class Elf : IRace
{
    public static readonly Elf Instance = new Elf();

    private Elf()
    {
    }

    public string Name => "Elf";

    public string Description => "Elves are a magical people of otherworldly grace, living in the world but not entirely part of it.";

    public ICreatureType CreatureType => Races.CreatureType.Humanoid;

    public ISize Size => Races.Size.Medium;

    public List<ATrait> RaceTraits { get; } = new List<ATrait>();
}
