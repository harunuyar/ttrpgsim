namespace Dnd.Predefined.Traits.Dwarf;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.CommandSystem.Commands.BooleanResultCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
using Dnd.System.Entities.Advantage;
using Dnd.System.Entities.Items.Equipments.Weapons;

public class DwarvenResilience : ATrait
{
    public static readonly DwarvenResilience Instance = new DwarvenResilience();

    private DwarvenResilience() : base("Dwarven Resilience", "You have advantage on saving throws against poison, and you have resistance against poison damage.")
    {
    }

    public override void HandleCommand(DndCommand command)
    {
        if (command is GetSavingThrowModifier getSavingThrowModifier)
        {
            if (getSavingThrowModifier.DamageType == EDamageType.Poison)
            {
                getSavingThrowModifier.Result.BonusCollection.AddAdvantage(this, EAdvantage.Advantage);
            }
        }

        if (command is HasDamageResistance getDamageResistance)
        {
            if (getDamageResistance.DamageType == EDamageType.Poison)
            {
                getDamageResistance.Result.SetValue(this, true);
            }
        }
    }
}
