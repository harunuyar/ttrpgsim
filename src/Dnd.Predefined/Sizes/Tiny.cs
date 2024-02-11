namespace Dnd.Predefined.Sizes;

using Dnd.System.Entities.Races;

public class Tiny : ISize
{
    public static readonly Tiny Instance = new Tiny();

    private Tiny()
    {
    }

    public string Name => "Tiny";

    public string Description => "Tiny as feary dragons and imps.";
}
