namespace Dnd.System.Entities.Action;

using Dnd.System.Entities.Units;

public enum EActionDurationType : byte
{
    None,
    Action,
    BonusAction,
    Reaction,
    FreeAction,
    Timed
}


public class ActionDurationType
{
    public EActionDurationType Type { get; }

    public Duration? Duration { get; }

    public ActionDurationType(EActionDurationType type, Duration? duration)
    {
        Type = type;
        Duration = duration;
    }

    public static ActionDurationType Action => new ActionDurationType(EActionDurationType.Action, null);

    public static ActionDurationType BonusAction => new ActionDurationType(EActionDurationType.BonusAction, null);

    public static ActionDurationType Reaction => new ActionDurationType(EActionDurationType.Reaction, null);

    public static ActionDurationType FreeAction => new ActionDurationType(EActionDurationType.FreeAction, null);

    public static ActionDurationType Timed(Duration duration) => new ActionDurationType(EActionDurationType.Timed, duration);

    public static ActionDurationType? FromString(string? actionTypeString)
    {
        if (string.IsNullOrWhiteSpace(actionTypeString))
        {
            return null;
        }

        if (actionTypeString == "1 action")
        {
            return Action;
        }
        else if (actionTypeString == "1 bonus action")
        {
            return BonusAction;
        }
        else if (actionTypeString == "1 reaction")
        {
            return Reaction;
        }
        else if (actionTypeString == "free action")
        {
            return FreeAction;
        }
        else
        {
            var duration = Duration.FromString(actionTypeString);
            if (duration != null)
            {
                return Timed(duration);
            }
        }

        return null;
    }
}
