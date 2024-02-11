namespace Dnd.Predefined.Alignments;

using Dnd.System.Entities.Allignments;

public class LawfulEvil : IAlignment
{
    public string Name => "Lawful Evil";

    public string Description => "Lawful evil (LE) creatures methodically take what they want, within the limits of a code of tradition, loyalty, or order. Devils, blue dragons, and hobgoblins are lawful evil.";

    private LawfulEvil() { }

    public static readonly LawfulEvil Instance = new LawfulEvil();
}
