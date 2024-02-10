namespace Dnd.Entities.Races.Predefined;

using Dnd.Entities.Traits;
using Dnd.Entities.Traits.Human;

public class Human : IRace
{
    public static readonly Human Instance = new Human();

    private Human()
    {
    }

    public string Name => "Human";

    public string Description => "Humans are the most adaptable and ambitious people";

    public ICreatureType CreatureType => Races.CreatureType.Humanoid;

    public ISize Size => Races.Size.Medium;

    public List<ATrait> RaceTraits { get; } = new List<ATrait>() { AbilityScoreIncrease.Instance };
}
