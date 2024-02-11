namespace Dnd.Predefined.CreatureTypes;

using Dnd.System.Entities.Races;

public class Fey : ICreatureType
{
    public string Name => "Fey";

    public string Description => "Fey are magical creatures closely tied to the forces of nature. They dwell in twilight groves and misty forests.";

    public static readonly Fey Instance = new Fey();

    private Fey() { }
}
