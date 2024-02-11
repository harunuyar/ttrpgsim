namespace Dnd.Predefined.Alignments;

using Dnd.System.Entities.Allignments;

public class NeutralEvil : IAlignment
{
    public string Name => "Neutral Evil";

    public string Description => "Neutral evil (NE) is the alignment of those who do whatever they can get away with, without compassion or qualms. Many drow, some cloud giants, and goblins are neutral evil.";

    private NeutralEvil() { }

    public static readonly NeutralEvil Instance = new NeutralEvil();
}
