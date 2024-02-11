namespace Dnd.Predefined.Sizes;

using Dnd.System.Entities.Races;

public class Medium : ISize
{
    public static readonly Medium Instance = new Medium();

    private Medium()
    {
    }

    public string Name => "Medium";

    public string Description => "Medium as humans, elves, mind flayers";
}
