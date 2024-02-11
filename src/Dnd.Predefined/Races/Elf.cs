namespace Dnd.Predefined.Races;

using Dnd.System.Entities.Races;
using Dnd.System.Entities.Traits;
using Dnd.Predefined.CreatureTypes;
using Dnd.Predefined.Sizes;

public class Elf : IRace
{
    public static readonly Elf Instance = new Elf();

    private Elf()
    {
    }

    public string Name => "Elf";

    public string Description => "Elves are a magical people of otherworldly grace, living in the world but not entirely part of it.";

    public ICreatureType CreatureType => Humanoid.Instance;

    public ISize Size => Medium.Instance;

    public List<ITrait> RaceTraits { get; } = new List<ITrait>();
}
