namespace Dnd.Predefined.Effects;

using Dnd.Predefined.Commands.VoidCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Effect;
using Dnd.System.Entities.GameActor;
using Dnd.System.Entities.Units;

public abstract class AEffect : IEffect
{
    public AEffect(string name, string desc, EffectDuration duration, IGameActor source)
    {
        Name = name;
        Description = desc;
        Duration = duration;
        Source = source;

        if (duration.Type.HasFlag(EEffectDurationType.UntilTriggered))
        {
            RemainingTriggerCount = duration.TriggerCount;
        }

        if (duration.Type.HasFlag(EEffectDurationType.UntilShortRest) || duration.Type.HasFlag(EEffectDurationType.UntilLongRest))
        {
            RemainingRestCount = duration.RestCount;
        }

        if (duration.Type.HasFlag(EEffectDurationType.Timed))
        {
            RemainingDuration = duration.Duration?.Clone();
        }
    }

    public string Name { get; }

    public string Description { get; }

    public EffectDuration Duration { get; }

    public IGameActor Source { get; }

    public Duration? RemainingDuration { get; }

    public int? RemainingTriggerCount { get; private set; }

    public int? RemainingRestCount { get; private set; }

    public bool IsExpired => RemainingDuration is not null 
        ? RemainingDuration.IsOver 
        : (RemainingTriggerCount.HasValue 
            ? RemainingTriggerCount <= 0 
            : (RemainingRestCount.HasValue && RemainingRestCount <= 0));

    public virtual Task ActivateEffect()
    {
        if (Duration.Type.HasFlag(EEffectDurationType.UntilTriggered) && RemainingTriggerCount.HasValue)
        {
            RemainingTriggerCount--;
        }

        return Task.CompletedTask;
    }

    public virtual Task HandleCommand(ICommand command)
    {
        if (command is TakeTurn takeTurn)
        {
            if (takeTurn.Actor == Source)
            {
                RemainingDuration?.PassTurn();
            }
        }
        else if (command is PassTime passTime)
        {
            if (passTime.Actor == Source)
            {
                RemainingDuration?.PassTime(passTime.TimeSpan);
            }
        }
        else if (command is LongRest)
        {
            if (Duration.Type.HasFlag(EEffectDurationType.UntilLongRest) && RemainingRestCount.HasValue)
            {
                RemainingRestCount--;
            }
        }
        else if (command is ShortRest)
        {
            if (Duration.Type.HasFlag(EEffectDurationType.UntilShortRest) && RemainingRestCount.HasValue)
            {
                RemainingRestCount--;
            }
        }

        return Task.CompletedTask;
    }
}
