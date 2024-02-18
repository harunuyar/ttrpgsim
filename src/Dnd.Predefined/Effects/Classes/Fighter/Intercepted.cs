namespace Dnd.Predefined.Effects.Classes.Fighter;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.CommandSystem.Commands.EventCommands;
using Dnd.System.Entities.Effects.Duration;
using Dnd.System.Entities.GameActors;

public class Intercepted : AEffect
{
    public Intercepted(IGameActor source, IGameActor target, int reducedDamage) : base("Intercepted Attack", $"The damage will be reduced by {reducedDamage}.", new UntilTriggered(1), source, target)
    {
    }

    public int ReducedDamage { get; }

    public override void HandleCommand(ICommand command)
    {
        if (command is ApplyDamage applyDamage)
        {
            applyDamage.Damage = Math.Max(0, applyDamage.Damage - ReducedDamage);
            Source.EffectsTable.RemoveCausedEffect(this);
        }
        else if (command is TakeTurn)
        {
            Source.EffectsTable.RemoveCausedEffect(this);
        }
    }
}
