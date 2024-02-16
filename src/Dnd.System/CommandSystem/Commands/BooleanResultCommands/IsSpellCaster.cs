namespace Dnd.System.CommandSystem.Commands.BooleanResultCommands;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.Entities.GameActors;

public class IsSpellCaster : DndBooleanCommand
{
    public IsSpellCaster(IGameActor character) : base(character)
    {
    }

    protected override void InitializeResult()
    {
        Result.SetValue(false, $"{Actor.Name} is not a spell caster.");
    }
}
