namespace Dnd.Predefined.Races;

using Dnd.System.Entities.Races;
using Dnd.Predefined.CreatureTypes;
using Dnd.Predefined.Sizes;
using Dnd.Predefined.Traits.Dragonborn;
using Dnd.System.Entities.Traits;
using Dnd.System.Entities.Units;

public class Dragonborn : IRace
{
    public Dragonborn()
    {
    }

    public string Name => "Dragonborn";

    public string Description => "Dragonborn look very much like dragons standing erect in humanoid form, though they lack wings or a tail.";

    public ICreatureType CreatureType => Humanoid.Instance;

    public ISize Size => Medium.Instance;

    public List<ITrait> RaceTraits { get; } = new List<ITrait>() { AbilityScoreIncrease.Instance };

    public Distance Speed => Distance.OfFeet(30);
}
