namespace Dnd.Predefined.Sizes;

public class Huge
{
    public static readonly Huge Instance = new Huge();

    private Huge()
    {
    }

    public string Name => "Huge";

    public string Description => "Huge as adult dragons";
}
