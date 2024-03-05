namespace Dnd.Predefined.Actions;

using Dnd.Predefined.Commands.VoidCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action;

public class ActionUsageLimit : IActionUsageLimit
{
    public ActionUsageLimit(EActionUsageLimitType type, int limit)
    {
        Type = type;
        Limit = limit;
        Current = 0;
    }

    public EActionUsageLimitType Type { get; }
    public int Limit { get; }
    public int Current { get; private set; }

    public void Use()
    {
        Current++;
    }

    public Task HandleCommand(ICommand command)
    {
        if (command is ShortRest)
        {
            if (!Type.HasFlag(EActionUsageLimitType.PerLongRest))
            {
                Current = 0;
            }
        }
        else if (command is LongRest)
        {
            Current = 0;
        }
        else if (command is TakeTurn)
        {
            if (Type.HasFlag(EActionUsageLimitType.PerTurn))
            {
                Current = 0;
            }
        }
        else if (command is NewRound)
        {
            if (Type.HasFlag(EActionUsageLimitType.PerRound))
            {
                Current = 0;
            }
        }
        else if (command is NewCombat)
        {
            if (Type.HasFlag(EActionUsageLimitType.PerCombat))
            {
                Current = 0;
            }
        }

        return Task.CompletedTask;
    }
}
