namespace Dnd.System.CommandSystem.Commands.BooleanResultCommands;

using Dnd.System.Entities.GameActors;

public class CanHeal : DndBooleanCommand
{
    public CanHeal(IGameActor character) : base(character)
    {
    }

    protected override void InitializeResult()
    {
        Result.SetValue("Default", true);
    }
}
