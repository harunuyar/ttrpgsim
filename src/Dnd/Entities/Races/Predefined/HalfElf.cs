namespace Dnd.Entities.Races.Predefined;

using Dnd.Entities.Attributes;
using Dnd.Entities.Traits;

public class HalfElf : IRace
{
    public static HalfElf Instance(EAttributeType attribute1, EAttributeType attribute2) => new HalfElf(attribute1, attribute2);

    private HalfElf(EAttributeType attribute1, EAttributeType attribute2)
    {
        RaceTraits.Add(Traits.HalfElf.AbilityScoreIncrease.Instance(attribute1, attribute2));
    }

    public string Name => "Half-Elf";

    public string Description => "Half-elves combine what some say are the best qualities of their elf and human parents.";

    public ICreatureType CreatureType => Races.CreatureType.Humanoid;

    public ISize Size => Races.Size.Medium;

    public List<ATrait> RaceTraits { get; } = new List<ATrait>();
}
