namespace DnD.Entities;

internal static class EAdvantageExtensions
{
    public static EAdvantage Combine(this EAdvantage a, EAdvantage b)
    {
        return a | b;
    }

    public static bool HasAdvantage(this EAdvantage a)
    {
        return a == EAdvantage.Advantage;
    }

    public static bool HasDisadvantage(this EAdvantage a)
    {
        return a == EAdvantage.Disadvantage;
    }
}
