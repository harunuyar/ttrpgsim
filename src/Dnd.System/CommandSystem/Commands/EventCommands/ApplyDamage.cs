namespace Dnd.System.CommandSystem.Commands.EventCommands;

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

    protected override void FinalizeResult()
    {
        Actor.HitPoints.Damage(Damage);
        Result.SetMessage($"Dealt {Damage} damage to {Actor.Name}");
    }
}
