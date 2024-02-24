namespace Dnd.Predefined.Commands.VoidCommands;

using Dnd._5eSRD.Models.DamageType;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class ApplyDamage : VoidCommand
{
    public ApplyDamage(IGameActor character, int damage, DamageTypeModel damageType) : base(character)
    {
        Damage = damage;
        DamageType = damageType;
    }

    public DamageTypeModel DamageType { get; }

    public int Damage { get; set; }

    protected override Task FinalizeResult()
    {
        Actor.HitPoints.Damage(Damage);
        SetMessage($"Dealt {Damage} damage to {Actor.Name}.");

        return Task.CompletedTask;
    }
}
