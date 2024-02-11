namespace Dnd.Predefined.Alignments;

using Dnd.System.Entities.Allignments;

public class ChaoticGood : IAlignment
{
    public string Name => "Chaotic Good";

    public string Description => "Chaotic good (CG) creatures act as their conscience directs, with little regard for what others expect. Many barbarians and rogues, and some bards, are chaotic good.";

    private ChaoticGood() { }

    public static readonly ChaoticGood Instance = new ChaoticGood();
}
