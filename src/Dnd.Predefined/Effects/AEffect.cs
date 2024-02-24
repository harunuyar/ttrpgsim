namespace Dnd.Predefined.Effects;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Effect;
using Dnd.System.Entities.GameActor;
using Dnd.System.Entities.Units;

public class AEffect : IEffect
{
    public AEffect(string name, string desc, EffectDurationType durationType, IGameActor source, IGameActor target, TimeSpan? duration = null, int? maxTriggerCount = null, int? maxRestCount = null)
    {
        Name = name;
        Description = desc;
        DurationType = durationType;
        Source = source;
        Target = target;
        Duration = duration;
        MaxTriggerCount = maxTriggerCount;
        MaxRestCount = maxRestCount;
    }

    public string Name { get; }

    public string Description { get; }

    public EffectDurationType DurationType { get; }

    public IGameActor Source { get; }

    public IGameActor Target { get; }

    public TimeSpan? Duration { get; }

    public TimeSpan? RemainingDuration { get; }

    public int? MaxTriggerCount { get; }

    public int? RemainingTriggerCount { get; }

    public int? MaxRestCount { get; }

    public int? RemainingRestCount { get; }

    public virtual Task HandleCommand(ICommand command)
    {
        // TODO: handle turn start/end, round start/end, etc.
        return Task.CompletedTask;
    }
}
