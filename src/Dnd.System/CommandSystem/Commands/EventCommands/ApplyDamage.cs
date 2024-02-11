namespace Dnd.CommandSystem.Commands.EventCommands;

using Dnd.CommandSystem.Commands.IntegerResultCommands;
using Dnd.CommandSystem.Results;
using Dnd.Entities.Characters;
using Dnd.Entities.Items.Equipments.Weapons;

public class ApplyDamage : DndCommand
{
    public ApplyDamage(Character character, int damage, EDamageType damageType) : base(character)
    {
        Damage = damage;
        DamageType = damageType;
    }

    public int Damage { get; }

    public EDamageType DamageType { get; }

    public override ICommandResult Execute()
    {
        var calculateDamage = new CalculateDamage(Character, Damage, DamageType);
        var damageResult = calculateDamage.Execute();

        if (damageResult.IsSuccess)
        {
            Character.HitPoints.Damage(damageResult.Value);
            return EventResult.Success(this);
        }
        else
        {
            return EventResult.Failure(this, damageResult.ErrorMessage ?? "Unknown");
        }
    }
}
