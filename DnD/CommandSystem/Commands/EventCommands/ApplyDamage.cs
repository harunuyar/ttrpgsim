namespace DnD.CommandSystem.Commands.EventCommands;

using DnD.CommandSystem.Commands.IntegerResultCommands;
using DnD.CommandSystem.Results;
using DnD.Entities.Characters;
using DnD.Entities.Items.Equipments.Weapons;

internal class ApplyDamage : DndCommand
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
