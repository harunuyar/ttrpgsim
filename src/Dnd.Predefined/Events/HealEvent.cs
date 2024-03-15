namespace Dnd.Predefined.Events;

using Dnd.Predefined.Commands.ScoreCommands;
using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;

public class HealEvent : AEvent
{
    public HealEvent(string name, IGameActor eventOwner, IHealAction healAction, int? amount) 
        : base(name, eventOwner)
    {
        Amount = amount;
        HealAction = healAction;
    }

    public override bool IsWaitingForUserInput => Amount is null;

    public int? Amount { get; }

    public ScoreResult? CalculatedHealing { get; set; }

    public IHealAction HealAction { get; }

    public override async Task<IEnumerable<IEvent>> RunEventImpl()
    {
        CalculatedHealing = await new CalculateHeal(EventOwner, Amount!.Value).Execute();

        if (!CalculatedHealing.IsSuccess)
        {
            throw new InvalidOperationException($"Failed to calculate damage for {EventOwner.Name}");
        }

        EventOwner.HitPoints.Heal(CalculatedHealing.Value);
        SetEventPhase(EEventPhase.DoneRunning);
        return await Task.FromResult(Enumerable.Empty<IEvent>());
    }
}
