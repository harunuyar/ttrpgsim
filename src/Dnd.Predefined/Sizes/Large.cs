namespace Dnd.Predefined.Sizes;

using Dnd.System.Entities.Races;

public class Large : ISize
{
    public static readonly Large Instance = new Large();

    private Large()
    {
    }

    public string Name => "Large";

    public string Description => "Large as aboleths, ogres";
}
