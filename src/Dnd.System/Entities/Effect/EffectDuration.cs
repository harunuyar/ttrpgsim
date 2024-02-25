namespace Dnd.System.Entities.Effect;

using Dnd.System.Entities.Units;

public class EffectDuration
{
    public EffectDuration(EEffectDurationType type, Duration? duration = null, int? triggerCount = null, int? restCount = null)
    {
        Type = type;
        Duration = duration;
        TriggerCount = triggerCount;
        RestCount = restCount;
    }

    public EEffectDurationType Type { get; }

    public Duration? Duration { get; }

    public int? TriggerCount { get; }

    public int? RestCount { get; }

    public static EffectDuration Instantaneous => new EffectDuration(EEffectDurationType.Instantaneous);

    public static EffectDuration Permanent => new EffectDuration(EEffectDurationType.Permanent);

    public static EffectDuration UntilDispelled => new EffectDuration(EEffectDurationType.UntilDispelled);

    public static EffectDuration UntilTriggered => new EffectDuration(EEffectDurationType.UntilTriggered);

    public static EffectDuration UntilShortRest => new EffectDuration(EEffectDurationType.UntilShortRest);

    public static EffectDuration UntilLongRest => new EffectDuration(EEffectDurationType.UntilLongRest);

    public static EffectDuration UntilBroken => new EffectDuration(EEffectDurationType.UntilBroken);

    public static EffectDuration Timed(Duration duration) => new EffectDuration(EEffectDurationType.Timed, duration);

    public static EffectDuration Special => new EffectDuration(EEffectDurationType.Special);

    public static EffectDuration? FromString(string? durationString)
    {
        if (string.IsNullOrWhiteSpace(durationString))
        {
            return null;
        }

        durationString = durationString.Replace("Up to ", string.Empty);

        if (durationString.Equals("Instantaneous", StringComparison.OrdinalIgnoreCase))
        {
            return Instantaneous;
        }

        if (durationString.Equals("Permanent", StringComparison.OrdinalIgnoreCase))
        {
            return Permanent;
        }

        if (durationString.Equals("Until dispelled", StringComparison.OrdinalIgnoreCase))
        {
            return UntilDispelled;
        }

        if (durationString.Equals("Until triggered", StringComparison.OrdinalIgnoreCase))
        {
            return UntilTriggered;
        }

        if (durationString.Equals("Until short rest", StringComparison.OrdinalIgnoreCase))
        {
            return UntilShortRest;
        }

        if (durationString.Equals("Until long rest", StringComparison.OrdinalIgnoreCase))
        {
            return UntilLongRest;
        }

        if (durationString.Equals("Until broken", StringComparison.OrdinalIgnoreCase))
        {
            return UntilBroken;
        }

        if (durationString.Equals("Special", StringComparison.OrdinalIgnoreCase))
        {
            return Special;
        }

        var duration = Duration.FromString(durationString);
        if (duration != null)
        {
            return Timed(duration);
        }

        return null;
    }
}
