namespace Dnd.Predefined.CreatureTypes;

using Dnd.System.Entities.Races;

public class Celestial : ICreatureType
{
    public string Name => "Celestial";

    public string Description => "Celestials are creatures native to the Upper Planes. Many of them are the servants of deities, employed as messengers, soldiers, spies, and assassins.";

    public static readonly Celestial Instance = new Celestial();

    private Celestial() { }
}
