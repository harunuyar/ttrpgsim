namespace Dnd.Predefined.CreatureTypes;

using Dnd.System.Entities.Races;

public class Beast : ICreatureType
{
    public string Name => "Beast";

    public string Description => "Beasts are nonhumanoid creatures that are a natural part of the fantasy ecology. Some of them have magical powers, but most are unintelligent and lack any society or language.";

    public static readonly Beast Instance = new Beast();

    private Beast() { }
}
