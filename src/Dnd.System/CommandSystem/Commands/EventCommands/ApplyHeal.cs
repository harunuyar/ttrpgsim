namespace Dnd.System.CommandSystem.Commands.EventCommands;

using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities.GameActors;

public class ApplyHeal : DndEventCommand
{
    public ApplyHeal(IEventListener eventListener, IGameActor character, int amount) : base(eventListener, character)
    {
        Amount = amount;
    }

    public int Amount { get; }

    protected override void InitializeEvent()
    {
    }

    protected override void FinalizeEvent()
    {
        var calculateHeal = new CalculateHealAmount(Character, Amount);
        var healAmountResult = calculateHeal.Execute();

        if (healAmountResult.IsSuccess)
        {
            Character.HitPoints.Heal(healAmountResult.Value);
            EventResult.SetMessage($"Healed {Character.Name} by {healAmountResult.Value}");
        }
        else
        {
            EventResult.SetError(healAmountResult.ErrorMessage ?? "Unknown");
        }
    }
}
