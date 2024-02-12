namespace Dnd.System.Entities.Effects.Duration;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.CommandSystem.Commands.EventCommands;

public class UntilDispelled : IEffectDuration
{
    public UntilDispelled()
    {
        IsDispelled = false;
    }

    private bool IsDispelled { get; set; }

    public void HandleCommand(ICommand command)
    {
    }

    public bool IsExpired()
    {
        return IsDispelled;
    }
}
