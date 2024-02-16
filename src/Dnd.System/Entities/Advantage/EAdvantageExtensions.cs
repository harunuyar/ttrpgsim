namespace Dnd.System.Entities.Advantage;

public static class EAdvantageExtensions
{
    public static bool HasAdvantage(this EAdvantage a)
    {
        return a == EAdvantage.Advantage;
    }

    public static bool HasDisadvantage(this EAdvantage a)
    {
        return a == EAdvantage.Disadvantage;
    }

    public static bool IsEmpty(this EAdvantage a)
    {
        return a == EAdvantage.None;
    }
}
