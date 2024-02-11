namespace Dnd.Predefined.Alignments;

using Dnd.System.Entities.Allignments;

public class LawfulGood : IAlignment
{
    public string Name => "Lawful Good";

    public string Description => "Lawful good (LG) creatures can be counted on to do the right thing as expected by society. Gold dragons, paladins, and most dwarves are lawful good.";

    private LawfulGood() { }

    public static LawfulGood Instance { get; } = new LawfulGood();
}
