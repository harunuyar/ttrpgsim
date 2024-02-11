namespace Dnd.Entities.Races.Predefined;

using Dnd.Entities.Traits;
using Dnd.Entities.Traits.Dwarf;

public class Dwarf : IRace
{
    public static readonly Dwarf Instance = new Dwarf();

    private Dwarf()
    {
    }

    public string Name => "Dwarf";

    public string Description => "Bold and hardy, dwarves are known as skilled warriors, miners, and workers of stone and metal.";

    public ICreatureType CreatureType => Races.CreatureType.Humanoid;

    public ISize Size => Races.Size.Medium;

    public List<ATrait> RaceTraits { get; } = new List<ATrait>() 
    { 
        AbilityScoreIncrease.Instance, 
        DwarvenResilience.Instance, 
        DwarvenCombatTraining.Instance 
    };
}
