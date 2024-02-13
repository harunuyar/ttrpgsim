namespace Dnd.System.CommandSystem.Commands.EventCommands;

using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Items.Equipments.Weapons;

public class ApplyDamage : DndEventCommand
{
    public ApplyDamage(IEventListener eventListener, IGameActor character, int damage, EDamageType damageType) : base(eventListener, character)
    {
        Damage = damage;
        DamageType = damageType;
    }

    public EDamageType DamageType { get; }

    public int Damage { get; }

    protected override void InitializeEvent()
    {
    }

    protected override void FinalizeEvent()
    {
        Character.HitPoints.Damage(Damage);
        EventResult.SetMessage($"Dealt {Damage} damage to {Character.Name}");
    }
}
