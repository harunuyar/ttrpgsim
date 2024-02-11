namespace Dnd.Predefined.Races;

using Dnd.System.Entities.Races;
using Dnd.System.Entities.Traits;
using Dnd.Predefined.CreatureTypes;
using Dnd.Predefined.Sizes;
using Dnd.Predefined.Traits.Human;

public class Human : IRace
{
    public static readonly Human Instance = new Human();

    private Human()
    {
    }

    public string Name => "Human";

    public string Description => "Humans are the most adaptable and ambitious people";

    public ICreatureType CreatureType => Humanoid.Instance;

    public ISize Size => Medium.Instance;

    public List<ITrait> RaceTraits { get; } = new List<ITrait>() { AbilityScoreIncrease.Instance };
}
