namespace Dnd.Predefined.CreatureTypes;

using Dnd.System.Entities.Races;

public class Humanoid : ICreatureType
{
    public string Name => "Humanoid";

    public string Description => "Humanoids are the main peoples of a fantasy gaming world, both civilized and savage, including humans and a tremendous variety of other species.";

    public static readonly Humanoid Instance = new Humanoid();

    private Humanoid() { }
}
