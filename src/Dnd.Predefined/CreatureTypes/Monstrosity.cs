namespace Dnd.Predefined.CreatureTypes;

using Dnd.System.Entities.Races;

public class Monstrosity : ICreatureType
{
    public string Name => "Monstrosity";

    public string Description => "Monstrosities are monsters in the strictest sense—frightening creatures that are not ordinary, not truly natural, and almost never benign.";

    public static readonly Monstrosity Instance = new Monstrosity();

    private Monstrosity() { }
}
