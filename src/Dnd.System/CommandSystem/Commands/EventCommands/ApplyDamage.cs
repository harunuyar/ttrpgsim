namespace Dnd.System.CommandSystem.Commands.EventCommands;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.Entities.Damage;
using Dnd.System.Entities.GameActors;
using Dnd.System.Events.EventListener;

public class ApplyDamage : DndEventCommand
{
    public ApplyDamage(IEventListener eventListener, IGameActor character, int damage, EDamageType damageType) : base(eventListener, character)
    {
        Damage = damage;
        DamageType = damageType;
    }

    public EDamageType DamageType { get; }

    public int Damage { get; set; }

    protected override void FinalizeResult()
    {
        Actor.HitPoints.Damage(Damage);
        Result.SetMessage($"Dealt {Damage} damage to {Actor.Name}.");
    }
}
