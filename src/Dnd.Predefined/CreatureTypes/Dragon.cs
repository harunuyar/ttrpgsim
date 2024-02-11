namespace Dnd.Predefined.CreatureTypes;

using Dnd.System.Entities.Races;

public class Dragon : ICreatureType
{
    public string Name => "Dragon";

    public string Description => "Dragons are large reptilian creatures of ancient origin and tremendous power. True dragons, including the good metallic dragons and the evil chromatic dragons, are highly intelligent and have innate magic.";

    public static readonly Dragon Instance = new Dragon();

    private Dragon() { }
}
