namespace Dnd.Predefined.Commands.BoolCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class CanHeal : ValueCommand<bool>
{
    public CanHeal(IGameActor character) : base(character)
    {
    }

    protected override Task InitializeResult()
    {
        SetValue(true, $"{Actor.Name} can heal.");

        return Task.CompletedTask;
    }
}
