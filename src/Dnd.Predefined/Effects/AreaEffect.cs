namespace Dnd.Predefined.Effects;

using Dnd._5eSRD.Models.Common;
using Dnd.Predefined.Commands.VoidCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Effect;
using Dnd.System.Entities.GameActor;

public abstract class AreaEffect : AEffect, IAreaEffect
{
    public AreaEffect(IActiveEffectDefinition effectDefinition, AreaOfEffectModel areaOfEffect, EffectDuration duration, IGameActor source, IEnumerable<IGameActor> actorsInArea)
        : base(effectDefinition, duration, source)
    {
        ActiveEffectDefinition = effectDefinition;
        AreaOfEffect = areaOfEffect;
        GameActorsInArea = new HashSet<IGameActor>(actorsInArea);
    }

    public IActiveEffectDefinition ActiveEffectDefinition { get; }

    public AreaOfEffectModel AreaOfEffect { get; }

    public HashSet<IGameActor> GameActorsInArea { get; }

    public async Task EnteredArea(IGameActor actor)
    {
        GameActorsInArea.Add(actor);
        actor.EffectsTable.AddAffectedAreaEffect(this);

        if (ActiveEffectDefinition.ActivationTime.HasFlag(EEffectActivationTime.TargetEnteredArea))
        {
            await ActivateForOneTarget(actor);
        }
    }

    public async Task LeftArea(IGameActor actor)
    {
        GameActorsInArea.Remove(actor);
        actor.EffectsTable.RemoveAffectedAreaEffect(this);

        if (ActiveEffectDefinition.ActivationTime.HasFlag(EEffectActivationTime.TargetLeftArea))
        {
            await ActivateForOneTarget(actor);
        }
    }

    public override async Task ActivateEffect()
    {
        foreach (var target in GameActorsInArea)
        {
            await ActiveEffectDefinition.Activate(Source, target);
        }

        await base.ActivateEffect();
    }

    public async Task ActivateForOneTarget(IGameActor target)
    {
        await ActiveEffectDefinition.Activate(Source, target);
        await base.ActivateEffect();
    }

    public override async Task HandleCommand(ICommand command)
    {
        await base.HandleCommand(command);

        if (command is TakeTurn)
        {
            if (ActiveEffectDefinition.ActivationTime.HasFlag(EEffectActivationTime.TargetTurnStart) && GameActorsInArea.Contains(command.Actor))
            {
                await ActivateForOneTarget(command.Actor);
            }
        }
        else if (command is EndTurn)
        {
            if (ActiveEffectDefinition.ActivationTime.HasFlag(EEffectActivationTime.TargetTurnEnd) && GameActorsInArea.Contains(command.Actor))
            {
                await ActivateForOneTarget(command.Actor);
            }
        }
        else
        if (command is LongRest)
        {
            if (command.Actor == Source)
            {
                Duration.LongRest();
            }
        }
        else if (command is ShortRest)
        {
            if (command.Actor == Source)
            {
                Duration.ShortRest();
            }
        }

        if (IsExpired)
        {
            Source.EffectsTable.RemoveCausedAreaEffect(this);
        }
    }
}
