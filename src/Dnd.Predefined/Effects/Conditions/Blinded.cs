namespace Dnd.Predefined.Effects.Conditions;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Effects.Duration;

public class Blinded : AEffect
{
    public Blinded(IEffectDuration duration, IGameActor source, IGameActor target)
        : base("Blinded", "A blinded creature can't see and automatically fails any ability check that requires sight. Attack rolls against the creature have advantage, and the creature's attack rolls have disadvantage.", duration, source, target)
    {
    }

    public override void HandleCommand(ICommand command)
    {
        base.HandleCommand(command);

        if (command is GetWeaponAttackModifier calculateWeaponAttackModifier)
        {
            calculateWeaponAttackModifier.AddAdvantage(this, System.Entities.Advantage.EAdvantage.Disadvantage);
        }
        else if (command is GetSpellAttackModifier calculateSpellAttackModifier)
        {
            calculateSpellAttackModifier.AddAdvantage(this, System.Entities.Advantage.EAdvantage.Disadvantage);
        }
        else if (command is GetWeaponAttackModifierAgainst calculateWeaponAttackModifierAgainstCharacter)
        {
            calculateWeaponAttackModifierAgainstCharacter.AddAdvantage(this, System.Entities.Advantage.EAdvantage.Advantage);
        }
        else if (command is GetSpellAttackModifierAgainst calculateSpellAttackModifierAgainstCharacter)
        {
            calculateSpellAttackModifierAgainstCharacter.AddAdvantage(this, System.Entities.Advantage.EAdvantage.Advantage);
        }
    }
}
