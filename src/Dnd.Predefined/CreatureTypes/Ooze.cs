namespace Dnd.Predefined.CreatureTypes;

using Dnd.System.Entities.Races;

public class Ooze : ICreatureType
{
    public string Name => "Ooze";

    public string Description => "Oozes are gelatinous creatures that rarely have a fixed shape. They are mostly subterranean, dwelling in caves and dungeons and feeding on refuse, carrion, or creatures unlucky enough to get in their way.";

    public static readonly Ooze Instance = new Ooze();

    private Ooze() { }
}
