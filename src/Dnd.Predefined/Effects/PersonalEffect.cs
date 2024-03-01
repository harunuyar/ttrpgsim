namespace Dnd.Predefined.Effects;

using Dnd.Predefined.Commands.VoidCommands;
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

    public override async Task ActivateEffect()
    {
        if (EffectDefinition is IActiveEffectDefinition activeEffect)
        {
            await activeEffect.Activate(Source, Target);
            await base.ActivateEffect();
        }
    }

    public override async Task HandleCommand(ICommand command)
    {
        await base.HandleCommand(command);

        if (command is TakeTurn)
        {
            if (EffectDefinition is IActiveEffectDefinition activeEffect && activeEffect.ActivationTime.HasFlag(EEffectActivationTime.TargetTurnStart) && command.Actor == Target)
            {
                await ActivateEffect();
            }
        }
        else if (command is EndTurn)
        {
            if (EffectDefinition is IActiveEffectDefinition activeEffect && activeEffect.ActivationTime.HasFlag(EEffectActivationTime.TargetTurnEnd) && command.Actor == Target)
            {
                await ActivateEffect();
            }
        }
        else if (command is LongRest)
        {
            if (EffectDefinition is IActiveEffectDefinition activeEffect && activeEffect.ActivationTime.HasFlag(EEffectActivationTime.OnLongRest) && command.Actor == Target)
            {
                await ActivateEffect();
            }

            if (command.Actor == Target)
            {
                Duration.LongRest();
            }
        }
        else if (command is ShortRest)
        {
            if (EffectDefinition is IActiveEffectDefinition activeEffect && activeEffect.ActivationTime.HasFlag(EEffectActivationTime.OnShortRest) && command.Actor == Target)
            {
                await ActivateEffect();
            }

            if (command.Actor == Target)
            {
                Duration.ShortRest();
            }
        }

        if (IsExpired)
        {
            Source.EffectsTable.RemoveCausedPersonalEffect(this);
        }
    }
}
