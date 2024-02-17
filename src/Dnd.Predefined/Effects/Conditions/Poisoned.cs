namespace Dnd.Predefined.Effects.Conditions;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Effects.Duration;
using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands.Modifiers;
using Dnd.System.Entities.DiceModifiers;

public class Poisoned : AEffect
{
    public Poisoned(IEffectDuration duration, IGameActor source, IGameActor target)
        : base("Poisoned", "A poisoned creature has disadvantage on attack rolls and ability checks.", duration, source, target)
    {
    }

    public override void HandleCommand(ICommand command)
    {
        if (command is GetAttributeModifier getAttributeModifier)
        {
            getAttributeModifier.AddAdvantage(this, EAdvantage.Disadvantage);
        }
        else if (command is GetAttackModifier getAttackModifier)
        {
            getAttackModifier.AddAdvantage(this, EAdvantage.Disadvantage);
        }
    }
}
