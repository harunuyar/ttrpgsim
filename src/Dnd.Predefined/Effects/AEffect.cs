namespace Dnd.Predefined.Effects;

using Dnd.Predefined.Commands.VoidCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Effect;
using Dnd.System.Entities.GameActor;

public abstract class AEffect : IEffect
{
    public AEffect(IEffectDefinition effectDefinition, EffectDuration duration, IGameActor source)
    {
        EffectDefinition = effectDefinition;
        Duration = duration.CreateInstance();
        Source = source;
    }

    public IGameActor Source { get; }

    public bool IsExpired => Duration.IsExpired;

    public IEffectDefinition EffectDefinition { get; }

    public EffectDurationInstance Duration { get; }

    public virtual Task ActivateEffect()
    {
        Duration.Trigger();

        if (IsExpired)
        {
            Source.EffectsTable.RemoveCausedEffect(this);
        }

        return Task.CompletedTask;
    }

    public virtual async Task HandleCommand(ICommand command)
    {
        if (command is TakeTurn)
        {
            if (EffectDefinition is IActiveEffectDefinition activeEffect && activeEffect.ActivationTime.HasFlag(EEffectActivationTime.SourceTurnStart) && command.Actor == Source)
            {
                await ActivateEffect();
            }

            if (command.Actor == Source)
            {
                Duration.PassTurn();
            }
        }
        else if (command is EndTurn)
        {
            if (EffectDefinition is IActiveEffectDefinition activeEffect && activeEffect.ActivationTime.HasFlag(EEffectActivationTime.SourceTurnEnd) && command.Actor == Source)
            {
                await ActivateEffect();
            }
        }
        else if (command is PassTime passTime)
        {
            if (command.Actor == Source)
            {
                Duration.PassTime(passTime.TimeSpan);
            }
        }
    }
}
