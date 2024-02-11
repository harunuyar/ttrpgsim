namespace Dnd.Predefined.Effects.Conditions;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Effects;
using Dnd.System.Entities.Effects.Duration;

public class Exhaustion : AEffect
{
    public Exhaustion(int level = 1)
        : base("Exhaustion", "Exhaustion is measured in six levels. An effect can give a creature one or more levels of exhaustion, as specified in the effect's description.", new UntilDispelled())
    {
        Level = level;
    }

    public int Level { get; set; }

    public override void HandleCommand(DndCommand command)
    {
        base.HandleCommand(command);
        // TODO: Implement exhaustion effects
    }
}