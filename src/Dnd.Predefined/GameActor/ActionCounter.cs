namespace Dnd.Predefined.GameActor;

using Dnd.Predefined.Commands.VoidCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class ActionCounter : IActionCounter
{
    public int ActionPoints { get; private set; }

    public int BonusActionPoints { get; private set; }

    public int ReactionPoints { get; private set; }

    public void Reset()
    {
        ActionPoints = 1;
        BonusActionPoints = 1;
        ReactionPoints = 1;
    }

    public void AddExtraActionPoint()
    {
        ActionPoints++;
    }

    public void UseActionPoint()
    {
        ActionPoints--;
    }

    public void UseBonusActionPoint()
    {
        BonusActionPoints--;
    }

    public void UseReactionPoint()
    {
        ReactionPoints--;
    }

    public Task HandleCommand(ICommand command)
    {
        if (command is TakeTurn)
        {
            Reset();
        }

        return Task.CompletedTask;
    }
}
