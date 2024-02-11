namespace Dnd.Predefined.Effects.Conditions;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
using Dnd.System.Entities.Effects;
using Dnd.System.Entities.Effects.Duration;

public class Blinded : AEffect
{
    public Blinded(IEffectDuration duration)
        : base("Blinded", "A blinded creature can't see and automatically fails any ability check that requires sight. Attack rolls against the creature have advantage, and the creature's attack rolls have disadvantage.", duration)
    {
    }

    public override void HandleCommand(DndCommand command)
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
