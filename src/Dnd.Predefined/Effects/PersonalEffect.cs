namespace Dnd.Predefined.Effects;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Effect;
using Dnd.System.Entities.GameActor;

public abstract class PersonalEffect : AEffect, IPersonalEffect
{
    public PersonalEffect(IEffectDefinition effectDefinition, EffectDuration duration, IGameActor source, IGameActor target)
        : base(effectDefinition, duration, source)
    {
        Target = target;
    }

    public IGameActor Target { get; }

    public override async Task HandleCommand(ICommand command)
    {
        await base.HandleCommand(command);
        await EffectDefinition.HandleCommand(command, Source, Target);
    }
}
