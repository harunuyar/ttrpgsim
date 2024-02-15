namespace Dnd.Predefined.Races;

using Dnd.System.Entities.Races;
using Dnd.System.Entities.Traits;
using Dnd.Predefined.CreatureTypes;
using Dnd.Predefined.Sizes;
using Dnd.Predefined.Traits.Dwarf;
using Dnd.System.Entities.Units;

public class Dwarf : IRace
{
    public Dwarf()
    {
    }

    public string Name => "Dwarf";

    public string Description => "Bold and hardy, dwarves are known as skilled warriors, miners, and workers of stone and metal.";

    public ICreatureType CreatureType => Humanoid.Instance;

    public ISize Size => Medium.Instance;

    public List<ITrait> RaceTraits { get; } = new List<ITrait>() 
    { 
        AbilityScoreIncrease.Instance, 
        DwarvenResilience.Instance, 
        DwarvenCombatTraining.Instance 
    };

    public Distance Speed => Distance.OfFeet(25);
}
