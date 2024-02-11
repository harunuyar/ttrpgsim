namespace Dnd.System.CommandSystem.Commands.EventCommands;

using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities.Characters;
using Dnd.System.Entities.Items.Equipments.Weapons;

public class ApplyDamage : DndCommand
{
    public ApplyDamage(ICharacter character, int damage, EDamageType damageType) : base(character)
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
