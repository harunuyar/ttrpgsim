namespace Dnd.Predefined.CreatureTypes;

using Dnd.System.Entities.Races;

public class Fiend : ICreatureType
{
    public string Name => "Fiend";

    public string Description => "Fiends are creatures of wickedness that are native to the Lower Planes. A few are the servants of deities, but many more labor under the leadership of archdevils and demon princes.";

    public static readonly Fiend Instance = new Fiend();

    private Fiend() { }
}
