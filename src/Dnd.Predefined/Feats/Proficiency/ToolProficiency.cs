namespace Dnd.Predefined.Feats.Proficiency;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.CommandSystem.Commands.BooleanResultCommands;
using Dnd.System.Entities.Items.Tools;

public class ToolProficiency : AFeat
{
    public ToolProficiency(EToolType toolType) : base("Tool Proficiency", GetDescription(toolType))
    {
        ToolType = toolType;
    }

    public EToolType ToolType { get; }

    public override void HandleCommand(ICommand command)
    {
        if (command is HasToolProficiency hasToolProficiency)
        {
            if (hasToolProficiency.ToolType.HasFlag(ToolType))
            {
                hasToolProficiency.SetValue(this, true, $"You have proficiency on {ToolType}");
            }
        }
    }

    private static string GetDescription(EToolType toolType)
    {
        var list = Enum.GetValues<EToolType>().Where(x => toolType.HasFlag(x)).Select(x => x.ToString());
        return $"You gain proficiency with the following tools: {string.Join(", ", list)}";
    }
}
