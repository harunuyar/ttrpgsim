namespace Dnd.System.CommandSystem.Commands.EventCommands;

using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities.Characters;

public class ApplyHeal : DndEventCommand
{
    public ApplyHeal(ICharacter character, int amount) : base(character)
    {
        Amount = amount;
    }

    public int Amount { get; }

    public override ICommandResult Execute()
    {
        var calculateHeal = new CalculateHealAmount(Character, Amount);
        var healAmountResult = calculateHeal.Execute();

        if (healAmountResult.IsSuccess)
        {
            Character.HitPoints.Heal(healAmountResult.Value);
            return EventResult.Success(this);
        }
        else
        {
            return EventResult.Failure(this, healAmountResult.ErrorMessage ?? "Unknown");
        }
    }
}
