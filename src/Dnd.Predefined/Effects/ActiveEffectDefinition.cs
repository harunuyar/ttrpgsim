namespace Dnd.Predefined.Effects;

using Dnd.Context;
using Dnd.Predefined.Commands.ListCommands;
using Dnd.Predefined.Events;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Effect;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;

public class ActiveEffectDefinition : IActiveEffectDefinition
{
    public ActiveEffectDefinition(string name, string description, EEffectActivationTime activationTime)
    {
        Name = name;
        Description = description;
        ActivationTime = activationTime;
    }

    public EEffectActivationTime ActivationTime { get; }

    public string Name { get; }

    public string Description { get; }

    public virtual IEffectEvent CreateEvent(IGameActor source, IGameActor target)
    {
        return new BasicEffectEvent(this, source, target, DndContext.Instance.Logger.Log($"{Name} effect is activated."));
    }

    public Task HandleCommand(ICommand command, IGameActor effectSource, IGameActor effectOwner)
    {
        if (command is GetEffectEvents effectEvents)
        {
            var activationTime = effectEvents.EffectActivationTime;
            if (effectEvents.Actor == effectSource)
            {
                activationTime |= EEffectActivationTime.Caster;
            }
            else if (effectEvents.Actor == effectOwner)
            {
                activationTime |= EEffectActivationTime.Target;
            }

            if (activationTime.HasFlag(ActivationTime))
            {
                effectEvents.AddValue(CreateEvent(effectSource, effectOwner), Name);
            }
        }

        return Task.CompletedTask;
    }
}
