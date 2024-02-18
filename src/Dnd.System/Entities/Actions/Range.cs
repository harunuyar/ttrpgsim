namespace Dnd.System.Entities.Actions;

using Dnd.System.Entities.Units;

public class Range
{
    public Range(ERange rangeType, Distance? rangeDistance, Distance? areaDistance)
    {
        RangeType = rangeType;
        RangeDistance = rangeDistance;
        AreaDistance = areaDistance;
    }

    public ERange RangeType { get; }

    public Distance? RangeDistance { get; }

    public Distance? AreaDistance { get; }
}
