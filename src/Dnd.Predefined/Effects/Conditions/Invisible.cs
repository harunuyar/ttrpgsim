namespace Dnd.Predefined.Effects.Conditions;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Effects.Duration;
using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands.Modifiers;
using Dnd.System.Entities.DiceModifiers;

public class Invisible : AEffect
{
    public Invisible(IEffectDuration duration, IGameActor source, IGameActor target)
        : base("Invisible", "An invisible creature is impossible to see without the aid of magic or a special sense. For the purpose of hiding, the creature is heavily obscured. The creature's location can be detected by any noise it makes or any tracks it leaves. Attack rolls against the creature have disadvantage, and the creature's attack rolls have advantage.", 
            duration, source, target)
    {
    }

    public override void HandleCommand(ICommand command)
    {
        if (command is GetAttackModifier getAttackModifier)
        {
            getAttackModifier.AddAdvantage(this, EAdvantage.Advantage);
        }
        else if (command is GetAttackModifierAgainst getAttackModifierAgainst)
        {
            getAttackModifierAgainst.AddAdvantage(this, EAdvantage.Disadvantage);
        }
    }
}
