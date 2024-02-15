namespace Dnd.Predefined.Traits;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.CommandSystem.Commands.BooleanResultCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
using Dnd.System.Entities.Items.Equipments.Weapons;

public class Resistance : ATrait
{
    public Resistance(EDamageType damageType) : base("Damage Resistance", "If a creature or an object has resistance to a damage type, damage of that type is halved against it.")
    {
        DamageType = damageType;
    }

    public EDamageType DamageType { get; }

    public override void HandleCommand(ICommand command)
    {
        if (command is HasDamageResistance hasDamageResistance)
        {
            if (DamageType.HasFlag(hasDamageResistance.DamageType))
            {
                hasDamageResistance.SetValue(this, true, $"You have damage resistance for {hasDamageResistance.DamageType}.");
            }
        }
        else if (command is CalculateDamage calculateDamage)
        {
            if (calculateDamage.DamageType.HasFlag(DamageType))
            {
                calculateDamage.AddFinalAction(() =>
                {
                    if (calculateDamage.Result.Value > 0)
                    {
                        calculateDamage.AddBonus(this, -calculateDamage.Result.Value / 2);
                    }
                });
            }
        }
    }
}
