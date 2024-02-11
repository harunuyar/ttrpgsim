namespace Dnd.Predefined.Alignments;

using Dnd.System.Entities.Allignments;

public class NeutralGood : IAlignment
{
    public string Name => "Neutral Good";

    public string Description => "Neutral good (NG) folk do the best they can to help others according to their needs. Many celestials, some cloud giants, and most gnomes are neutral good.";

    private NeutralGood() { }

    public static readonly NeutralGood Instance = new NeutralGood();
}