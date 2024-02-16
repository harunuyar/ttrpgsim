namespace Dnd.System.CommandSystem.Commands.BooleanResultCommands;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.Entities.GameActors;

public class CanHeal : DndBooleanCommand
{
    public CanHeal(IGameActor character) : base(character)
    {
    }

    protected override void InitializeResult()
    {
        SetValue(true, $"{Actor.Name} can heal.");
    }
}
