namespace Dnd.Predefined.Events;

using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;

public class HealEvent : AEvent, IHealEvent
{
    public HealEvent(string name, IGameActor eventOwner, IHealAction? healAction, int amount) : base(name, eventOwner)
    {
        Amount = amount;
        HealAction = healAction;
    }

    public int Amount { get; }

    public IHealAction? HealAction { get; }

    public override async Task<IEnumerable<IEvent>> RunEvent()
    {
        EventOwner.HitPoints.Heal(Amount);
        SetEventPhase(EEventPhase.DoneRunning);
        return await Task.FromResult(Enumerable.Empty<IEvent>());
    }
}
