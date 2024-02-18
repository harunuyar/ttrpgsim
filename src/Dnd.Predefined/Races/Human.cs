namespace Dnd.Predefined.Races;

using Dnd.System.Entities.Races;
using Dnd.System.Entities.Traits;
using Dnd.Predefined.CreatureTypes;
using Dnd.Predefined.Sizes;
using Dnd.Predefined.Traits.Human;
using Dnd.System.Entities.Units;

public class Human : IRace
{
    public Human()
    {
    }

    public string Name => "Human";

    public string Description => "Humans are the most adaptable and ambitious people";

    public ICreatureType CreatureType => Humanoid.Instance;

    public ISize Size => Medium.Instance;

    public List<ITrait> RaceTraits { get; } = new List<ITrait>() { new AbilityScoreIncrease() };

    public Distance Speed => Distance.OfFeet(30);
}
