namespace Dnd.System.CommandSystem.Commands.EventCommands;

using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities.Characters;
using Dnd.System.Entities.Items.Equipments.Weapons;

public class ApplyDamage : DndEventCommand
{
    public ApplyDamage(IEventListener eventListener, ICharacter character, int damage, EDamageType damageType) : base(eventListener, character)
    {
        Damage = damage;
        DamageType = damageType;
    }

    public int Damage { get; }

    public EDamageType DamageType { get; }

    protected override void InitializeEvent()
    {
    }

    protected override void FinalizeEvent()
    {
        var calculateDamage = new CalculateDamage(Character, Damage, DamageType);
        var damageResult = calculateDamage.Execute();

        if (damageResult.IsSuccess)
        {
            Character.HitPoints.Damage(damageResult.Value);
            EventResult.SetMessage($"Dealt {damageResult.Value} damage to {Character.Name}");
        }
        else
        {
            EventResult.SetError(damageResult.ErrorMessage ?? "Unknown");
        }
    }
}
