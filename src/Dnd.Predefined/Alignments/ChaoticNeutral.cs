namespace Dnd.Predefined.Alignments;

using Dnd.System.Entities.Allignments;

public class ChaoticNeutral : IAlignment
{
    public string Name => "Chaotic Neutral";

    public string Description => "Chaotic neutral characters follow their whims. They are an individualist first and last. They value their own liberty but do not strive to protect the freedom of others. They avoid authority, resent restrictions, and challenge traditions. Chaotic neutral characters do not intentionally disrupt organizations as part of a campaign of anarchy. To do so, they would have to be motivated either by good (and a desire to liberate others) or evil (and a desire to make others suffer). Chaotic neutral characters may be unpredictable, but their behavior is not totally random. They are inot as likely to jump off a bridge as to cross it. Chaotic neutral characters can be very dangerous, but their respect for order and convention keeps them in check. They will not, for example, attempt to slay a king without a better reason than because the king's orders are considered to be a nuisance.";

    private ChaoticNeutral() { }

    public static readonly ChaoticNeutral Instance = new ChaoticNeutral();
}
