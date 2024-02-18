namespace Dnd.System.Entities.GameActors;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.CommandSystem.Commands.EventCommands;

public class ActionCounter
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

    public void HandleCommand(ICommand command)
    {
        if (command is TakeTurn)
        {
            Reset();
        }
    }
}
