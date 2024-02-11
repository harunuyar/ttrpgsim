namespace Dnd.Predefined.CreatureTypes;

using Dnd.System.Entities.Races;

public class Construct : ICreatureType
{
    public string Name => "Construct";

    public string Description => "Constructs are made, not born. Some are programmed by their creators to follow a simple set of instructions, while others are fully sentient and capable of independent thought.";

    public static readonly Construct Instance = new Construct();

    private Construct() { }
}
