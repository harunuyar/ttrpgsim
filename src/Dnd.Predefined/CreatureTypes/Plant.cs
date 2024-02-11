namespace Dnd.Predefined.CreatureTypes;

using Dnd.System.Entities.Races;

public class Plant : ICreatureType
{
    public string Name => "Plant";

    public string Description => "Plants in this context are vegetable creatures, not ordinary flora. Most of them are ambulatory, and some are carnivorous.";

    public static readonly Plant Instance = new Plant();

    private Plant() { }
}
