namespace Dnd.Predefined.Sizes;

using Dnd.System.Entities.Races;

public class Gargantuan : ISize
{
    public static readonly Gargantuan Instance = new Gargantuan();

    private Gargantuan()
    {
    }

    public string Name => "Gargantuan";

    public string Description => "Gargantuan as ancient dragons, purple worms";
}
