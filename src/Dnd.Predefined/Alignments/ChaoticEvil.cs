namespace Dnd.Predefined.Alignments;

using Dnd.System.Entities.Allignments;

public class ChaoticEvil : IAlignment
{
    public string Name => "Chaotic Evil";

    public string Description => "Chaotic evil (CE) creatures act with arbitrary violence, spurred by their greed, hatred, or bloodlust. Demons, red dragons, and orcs are chaotic evil.";

    private ChaoticEvil() { }

    public static readonly ChaoticEvil Instance = new ChaoticEvil();
}
