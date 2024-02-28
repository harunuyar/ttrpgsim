namespace Dnd.System.Entities.Effect;

using Dnd.System.Entities.Units;

public class EffectDurationInstance
{
    public EffectDurationInstance(EffectDuration effectDuration)
    {
        EffectDuration = effectDuration;
        RemainingDuration = effectDuration?.Duration?.Clone();
        RemainingRestCount = effectDuration?.RestCount;
        RemainingTriggerCount = effectDuration?.TriggerCount;
    }

    public EffectDuration EffectDuration { get; }
    public Duration? RemainingDuration { get; }
    public int? RemainingTriggerCount { get; private set; }
    public int? RemainingRestCount { get; private set; }
    public bool IsExpired => 
        RemainingDuration is not null ? RemainingDuration.IsOver 
            : (RemainingTriggerCount.HasValue ? RemainingTriggerCount <= 0 
                :(RemainingRestCount.HasValue ? RemainingRestCount <= 0 
                    : false));

    public void Trigger()
    {
        if (RemainingTriggerCount == null)
        {
            return;
        }

        RemainingTriggerCount--;
    }

    public void PassTime(Duration time)
    {
        if (RemainingDuration is not null)
        {
            RemainingDuration.PassTime(time);
        }
    }

    public void PassTime(TimeSpan time)
    {
        if (RemainingDuration is not null)
        {
            RemainingDuration.PassTime(time);
        }
    }

    public void PassTurn()
    {
        if (RemainingDuration is not null)
        {
            RemainingDuration.PassTurn();
        }
    }

    public void LongRest()
    {
        if (EffectDuration.Type.HasFlag(EEffectDurationType.UntilLongRest) && RemainingRestCount.HasValue)
        {
            RemainingRestCount--;
        }
    }

    public void ShortRest()
    {
        if (EffectDuration.Type.HasFlag(EEffectDurationType.UntilShortRest) && RemainingRestCount.HasValue)
        {
            RemainingRestCount--;
        }
    }
}
