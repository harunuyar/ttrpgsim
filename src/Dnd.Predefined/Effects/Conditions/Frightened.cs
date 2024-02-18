namespace Dnd.Predefined.Effects.Conditions;

using Dnd.System.Entities.DiceModifiers;
using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Effects.Duration;
using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands.Modifiers;

public class Frightened : AEffect
{
    public Frightened(IEffectDuration duration, IGameActor source, IGameActor target)
        : base("Frightened", "A frightened creature has disadvantage on ability checks and attack rolls while the source of its fear is within line of sight.", duration, source, target)
    {
    }

    public override void HandleCommand(ICommand command)
    {
        base.HandleCommand(command);

        if (command is GetAttackModifier getAttackModifier)
        {
            getAttackModifier.AddAdvantage(this, EAdvantage.Disadvantage);
        }
        else if (command is GetSavingThrowModifier getSavingThrowModifier)
        {
            getSavingThrowModifier.AddAdvantage(this, EAdvantage.Disadvantage);
        }
    }
}
