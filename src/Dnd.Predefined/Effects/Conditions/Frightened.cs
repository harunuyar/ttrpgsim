namespace Dnd.Predefined.Effects.Conditions;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
using Dnd.System.Entities.Advantage;
using Dnd.System.Entities.Characters;
using Dnd.System.Entities.Effects.Duration;

public class Frightened : AEffect
{
    public Frightened(IEffectDuration duration, ICharacter source, ICharacter target)
        : base("Frightened", "A frightened creature has disadvantage on ability checks and attack rolls while the source of its fear is within line of sight.", duration, source, target)
    {
    }

    public override void HandleCommand(ICommand command)
    {
        base.HandleCommand(command);

        if (command is CalculateWeaponAttackModifier calculateWeaponAttackModifier)
        {
            calculateWeaponAttackModifier.AddAdvantage(this, EAdvantage.Disadvantage);
        }
        else if (command is CalculateSpellAttackModifier calculateSpellAttackModifier)
        {
            calculateSpellAttackModifier.AddAdvantage(this, EAdvantage.Disadvantage);
        }
        else if (command is GetSavingThrowModifier getSavingThrowModifier)
        {
            getSavingThrowModifier.AddAdvantage(this, EAdvantage.Disadvantage);
        }
    }
}
