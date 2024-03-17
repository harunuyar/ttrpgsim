namespace Dnd.Predefined.Events;

using Dnd._5eSRD.Models.DamageType;
using Dnd.Predefined.Commands.ScoreCommands;
using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;

public class DamageEvent : AEvent
{
    public DamageEvent(string name, IGameActor eventOwner, DamageTypeModel damageType, int? amount) 
        : base(name, eventOwner)
    {
        DamageType = damageType;
        Amount = amount;
    }

    public override bool IsWaitingForUserInput => Amount is null;

    public DamageTypeModel DamageType { get; }

    public int? Amount { get; set; }

    public ScoreResult? CalculatedDamage { get; set; }

    public override async Task<IEnumerable<IEvent>> RunEventImpl()
    {
        CalculatedDamage = await new CalculateDamage(EventOwner, Amount!.Value, DamageType).Execute();

        if (!CalculatedDamage.IsSuccess)
        {
            throw new InvalidOperationException($"Failed to calculate damage for {EventOwner.Name}");
        }

        EventOwner.HitPoints.Damage(CalculatedDamage.Value);
        SetEventPhase(EEventPhase.DoneRunning);
        return [];
    }
}
