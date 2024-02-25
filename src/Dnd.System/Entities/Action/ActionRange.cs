namespace Dnd.System.Entities.Action;

using Dnd.System.Entities.Units;

public enum ERangeType
{
    Self,
    Touch,
    Ranged
}

public class ActionRange
{
    private ActionRange(ERangeType rangeType, Distance? rangeDistance)
    {
        RangeType = rangeType;
        RangeDistance = rangeDistance;
    }

    public ERangeType RangeType { get; set; }

    public Distance? RangeDistance { get; set; }

    public static ActionRange Self => new ActionRange(ERangeType.Self, null);

    public static ActionRange Touch => new ActionRange(ERangeType.Touch, null);

    public static ActionRange Ranged(Distance rangeDistance) => new ActionRange(ERangeType.Ranged, rangeDistance);

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
                return Ranged(Distance.OfFeet(distance));
            }
        }

        return null;
    }
}
