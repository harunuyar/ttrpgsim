namespace Dnd.Predefined.Races;

using Dnd.System.Entities.Races;
using Dnd.System.Entities.Traits;
using Dnd.Predefined.CreatureTypes;
using Dnd.Predefined.Sizes;
using Dnd.Predefined.Traits.Dwarf;

public class Dwarf : IRace
{
    public static readonly Dwarf Instance = new Dwarf();

    private Dwarf()
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
}
