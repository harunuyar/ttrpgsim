namespace Dnd.System.CommandSystem.Commands.EventCommands;

using Dnd.System.Entities.GameActors;

public class ApplyHeal : DndEventCommand
{
    public ApplyHeal(IEventListener eventListener, IGameActor character, int amount) : base(eventListener, character)
    {
        Amount = amount;
    }

    public int Amount { get; }

    protected override void FinalizeResult()
    {
        Actor.HitPoints.Heal(Amount);
        Result.SetMessage($"Healed {Actor.Name} by {Amount}");
    }
}
