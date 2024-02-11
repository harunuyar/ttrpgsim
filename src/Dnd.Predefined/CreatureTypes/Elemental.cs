namespace Dnd.Predefined.CreatureTypes;

using Dnd.System.Entities.Races;

public class Elemental : ICreatureType
{
    public string Name => "Elemental";

    public string Description => "Elementals are creatures native to the elemental planes.";

    public static readonly Elemental Instance = new Elemental();

    private Elemental() { }
}
