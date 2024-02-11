namespace Dnd.Predefined.Alignments;

using Dnd.System.Entities.Allignments;

public class TrueNeutral : IAlignment
{
    public string Name => "True Neutral";

    public string Description => "True neutral characters are concerned with their own well-being and that of the group or organization which aids them.";

    private TrueNeutral() { }

    public static readonly TrueNeutral Instance = new TrueNeutral();
}
