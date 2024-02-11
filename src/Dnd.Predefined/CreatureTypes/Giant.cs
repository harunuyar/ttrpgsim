namespace Dnd.Predefined.CreatureTypes;

using Dnd.System.Entities.Races;

public class Giant : ICreatureType
{
    public string Name => "Giant";

    public string Description => "Giants tower over humans and their kind. They are humanlike in shape, though some have multiple heads (ettins) or deformities (fomorians).";

    public static readonly Giant Instance = new Giant();

    private Giant() { }
}
