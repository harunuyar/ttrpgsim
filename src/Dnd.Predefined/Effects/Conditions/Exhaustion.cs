namespace Dnd.Predefined.Effects.Conditions;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Effects.Duration;
using Dnd.System.CommandSystem.Commands.BaseCommands;

public class Exhaustion : AEffect
{
    public Exhaustion(IEffectDuration duration, IGameActor source, IGameActor target, int level = 1)
        : base("Exhaustion", "Exhaustion is measured in six levels. An effect can give a creature one or more levels of exhaustion, as specified in the effect's description.", duration, source, target)
    {
        Level = level;
    }

    public int Level { get; set; }

    public override void HandleCommand(ICommand command)
    {
        base.HandleCommand(command);
        // TODO: Implement exhaustion effects
    }
}