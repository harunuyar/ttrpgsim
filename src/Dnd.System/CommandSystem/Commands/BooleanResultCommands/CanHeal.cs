namespace Dnd.System.CommandSystem.Commands.BooleanResultCommands;

using Dnd.System.Entities.GameActors;

public class CanHeal : DndBooleanCommand
{
    public CanHeal(IGameActor character) : base(character)
    {
    }

    protected override void InitializeResult()
    {
        SetValue(true, "By default, you can heal.");
    }
}
