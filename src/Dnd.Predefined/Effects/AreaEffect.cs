namespace Dnd.Predefined.Effects;

using Dnd._5eSRD.Models.Common;
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

    public override async Task HandleCommand(ICommand command)
    {
        await base.HandleCommand(command);

        foreach (var actor in GameActorsInArea)
        {
            await ActiveEffectDefinition.HandleCommand(command, Source, actor);
        }
    }
}
