namespace Dnd.Predefined.Effects.Conditions;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Effects.Duration;
using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands.Modifiers;

public class Blinded : AEffect
{
    public Blinded(IEffectDuration duration, IGameActor source, IGameActor target)
        : base("Blinded", "A blinded creature can't see and automatically fails any ability check that requires sight. Attack rolls against the creature have advantage, and the creature's attack rolls have disadvantage.", duration, source, target)
    {
    }

    public override void HandleCommand(ICommand command)
    {
        base.HandleCommand(command);

        if (command is GetAttackModifier getAttackModifier)
        {
            getAttackModifier.AddAdvantage(this, System.Entities.DiceModifiers.EAdvantage.Disadvantage);
        }
        else if (command is GetAttackModifierAgainst getAttackModifierAgainst)
        {
            getAttackModifierAgainst.AddAdvantage(this, System.Entities.DiceModifiers.EAdvantage.Advantage);
        }
    }
}
