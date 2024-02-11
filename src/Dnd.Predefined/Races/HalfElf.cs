namespace Dnd.Predefined.Races;

using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.Races;
using Dnd.System.Entities.Traits;
using Dnd.Predefined.CreatureTypes;
using Dnd.Predefined.Sizes;
using Dnd.Predefined.Traits.HalfElf;

public class HalfElf : IRace
{
    public static HalfElf Instance(EAttributeType attribute1, EAttributeType attribute2) => new HalfElf(attribute1, attribute2);

    private HalfElf(EAttributeType attribute1, EAttributeType attribute2)
    {
        RaceTraits.Add(AbilityScoreIncrease.Instance(attribute1, attribute2));
    }

    public string Name => "Half-Elf";

    public string Description => "Half-elves combine what some say are the best qualities of their elf and human parents.";

    public ICreatureType CreatureType => Humanoid.Instance;

    public ISize Size => Medium.Instance;

    public List<ITrait> RaceTraits { get; } = new List<ITrait>();
}
