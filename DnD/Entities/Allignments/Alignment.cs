namespace DnD.Entities.Allignments;

internal class Alignment : IAlignment
{
    public static readonly Alignment None = new Alignment("No Alignment");
    public static readonly Alignment LawfulGood = new Alignment("Lawful Good");
    public static readonly Alignment NeutralGood = new Alignment("Neutral Good");
    public static readonly Alignment ChaoticGood = new Alignment("Chaotic Good");
    public static readonly Alignment LawfulNeutral = new Alignment("Lawful Neutral");
    public static readonly Alignment TrueNeutral = new Alignment("True Neutral");
    public static readonly Alignment ChaoticNeutral = new Alignment("Chaotic Neutral");
    public static readonly Alignment LawfulEvil = new Alignment("Lawful Evil");
    public static readonly Alignment NeutralEvil = new Alignment("Neutral Evil");
    public static readonly Alignment ChaoticEvil = new Alignment("Chaotic Evil");

    private Alignment(string name)
    {
        Name = name;
    }

    public string Name { get; }
}
