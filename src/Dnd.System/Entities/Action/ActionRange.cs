namespace Dnd.System.Entities.Action;

using Dnd.System.Entities.Units;

public enum ERangeType
{
    Self,
    Touch,
    Melee,
    Ranged
}

public class ActionRange
{
    private ActionRange(ERangeType rangeType, Distance? rangeDistance, Distance? longRangeDistance)
    {
        RangeType = rangeType;
        NormalRangeDistance = rangeDistance;
        LongRangeDistance = longRangeDistance;
    }

    public ERangeType RangeType { get; set; }

    public Distance? NormalRangeDistance { get; set; }

    public Distance? LongRangeDistance { get; set; }

    public static ActionRange Self => new ActionRange(ERangeType.Self, null, null);

    public static ActionRange Touch => new ActionRange(ERangeType.Touch, null, null);

    public static ActionRange Melee(Distance rangeDistance, Distance? longRangeDistance) => new ActionRange(ERangeType.Melee, rangeDistance, longRangeDistance);

    public static ActionRange Ranged(Distance rangeDistance, Distance? longRangeDistance) => new ActionRange(ERangeType.Ranged, rangeDistance, longRangeDistance);

    public static ActionRange? FromString(string? rangeString)
    {
        if (string.IsNullOrWhiteSpace(rangeString))
        {
            return null;
        }

        if (rangeString == "Self")
        {
            return Self;
        }
        else if (rangeString == "Touch")
        {
            return Touch;
        }
        else
        {
            string distanceString = rangeString.Replace(" feet", "");
            if (int.TryParse(distanceString, out int distance))
            {
                return Ranged(Distance.OfFeet(distance), null);
            }
        }

        return null;
    }
}
