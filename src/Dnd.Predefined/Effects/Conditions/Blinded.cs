namespace Dnd.Predefined.Effects.Conditions;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
using Dnd.System.Entities.Characters;
using Dnd.System.Entities.Effects.Duration;

public class Blinded : AEffect
{
    public Blinded(IEffectDuration duration, ICharacter source, ICharacter target)
        : base("Blinded", "A blinded creature can't see and automatically fails any ability check that requires sight. Attack rolls against the creature have advantage, and the creature's attack rolls have disadvantage.", duration, source, target)
    {
    }

    public override void HandleCommand(ICommand command)
    {
        base.HandleCommand(command);

        if (command is CalculateWeaponAttackModifier calculateWeaponAttackModifier)
        {
            calculateWeaponAttackModifier.Result.BonusCollection.AddAdvantage(this, System.Entities.Advantage.EAdvantage.Disadvantage);
        }
        else if (command is CalculateSpellAttackModifier calculateSpellAttackModifier)
        {
            calculateSpellAttackModifier.Result.BonusCollection.AddAdvantage(this, System.Entities.Advantage.EAdvantage.Disadvantage);
        }
        else if (command is CalculateWeaponAttackModifierAgainstCharacter calculateWeaponAttackModifierAgainstCharacter)
        {
            calculateWeaponAttackModifierAgainstCharacter.Result.BonusCollection.AddAdvantage(this, System.Entities.Advantage.EAdvantage.Advantage);
        }
        else if (command is CalculateSpellAttackModifierAgainstCharacter calculateSpellAttackModifierAgainstCharacter)
        {
            calculateSpellAttackModifierAgainstCharacter.Result.BonusCollection.AddAdvantage(this, System.Entities.Advantage.EAdvantage.Advantage);
        }
    }
}
