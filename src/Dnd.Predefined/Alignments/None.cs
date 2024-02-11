namespace Dnd.Predefined.Alignments;

using Dnd.System.Entities.Allignments;

public class None : IAlignment
{
    public string Name => "No Alignment";

    public string Description => "No alignment specified";

    private None() { }

    public static readonly None Instance = new None();
}
