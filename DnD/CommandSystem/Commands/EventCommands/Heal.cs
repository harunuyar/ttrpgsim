namespace DnD.CommandSystem.Commands.EventCommands;

using DnD.CommandSystem.Commands.IntegerResultCommands;
using DnD.CommandSystem.Results;
using DnD.Entities.Characters;

internal class Heal : DndCommand
{
    public Heal(Character character, int amount) : base(character)
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
