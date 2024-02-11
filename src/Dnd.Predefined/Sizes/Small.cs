namespace Dnd.Predefined.Sizes;

using Dnd.System.Entities.Races;

public class Small : ISize
{
    public static readonly Small Instance = new Small();

    private Small()
    {
    }

    public string Name => "Small";

    public string Description => "Small as goblins, gnomes, kobolds, halflings";
}
