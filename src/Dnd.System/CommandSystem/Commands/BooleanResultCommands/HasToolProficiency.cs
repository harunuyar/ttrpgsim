namespace Dnd.System.CommandSystem.Commands.BooleanResultCommands;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Items.Tools;

public class HasToolProficiency : DndBooleanCommand
{
    public HasToolProficiency(IGameActor character, EToolType toolType) : base(character)
    {
        ToolType = toolType;
    }

    public EToolType ToolType { get; }

    protected override void InitializeResult()
    {
        Result.SetValue(null, false, $"By default, you don't have proficiency on {ToolType}");
    }
}
