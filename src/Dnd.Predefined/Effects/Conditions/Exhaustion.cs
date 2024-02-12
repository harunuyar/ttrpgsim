namespace Dnd.Predefined.Effects.Conditions;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Characters;
using Dnd.System.Entities.Effects.Duration;

public class Exhaustion : AEffect
{
    public Exhaustion(IEffectDuration duration, ICharacter source, ICharacter target, int level = 1)
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