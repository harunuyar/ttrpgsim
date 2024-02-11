namespace Dnd.Predefined.CreatureTypes;

using Dnd.System.Entities.Races;

public class Undead : ICreatureType
{
    public string Name => "Undead";

    public string Description => "The undead are beings that have died and yet still wander the world. Many undead, such as vampires and liches, are evil and fiendish creatures.";

    public static readonly Undead Instance = new Undead();

    private Undead() { }
}
