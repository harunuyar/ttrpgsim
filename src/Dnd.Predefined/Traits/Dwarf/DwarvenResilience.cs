﻿namespace Dnd.Predefined.Traits.Dwarf;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands.Modifiers;
using Dnd.System.Entities.DiceModifiers;
using Dnd.System.Entities.Damage;

public class DwarvenResilience : ATrait
{
    public DwarvenResilience() : base("Dwarven Resilience", "You have advantage on saving throws against poison, and you have resistance against poison damage.")
    {
        PoisonResistance = new Resistance(EDamageType.Poison);
    }

    public Resistance PoisonResistance { get; }

    public override void HandleCommand(ICommand command)
    {
        if (command is GetSavingThrowModifier getSavingThrowModifier)
        {
            if (getSavingThrowModifier.DamageType == EDamageType.Poison)
            {
                getSavingThrowModifier.AddAdvantage(this, EAdvantage.Advantage);
            }
        }

        PoisonResistance.HandleCommand(command);
    }
}
