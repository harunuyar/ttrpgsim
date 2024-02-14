namespace Dnd.System.CommandSystem.Commands.BooleanResultCommands;

using Dnd.System.Entities.GameActors;

public class IsSpellCaster : DndBooleanCommand
{
    public IsSpellCaster(IGameActor character) : base(character)
    {
    }

    protected override void InitializeResult()
    {
        Result.SetValue("Default", false);
    }
}
