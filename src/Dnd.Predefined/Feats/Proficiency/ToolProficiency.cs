namespace Dnd.Predefined.Feats.Proficiency;

using Dnd.System.Entities.Items.Tools;

public class ToolProficiency : AFeat
{
    public ToolProficiency(EToolType toolType) : base("Tool Proficiency", GetDescription(toolType))
    {
        ToolType = toolType;
    }

    public EToolType ToolType { get; }

    private static string GetDescription(EToolType toolType)
    {
        var list = Enum.GetValues<EToolType>().Where(x => toolType.HasFlag(x)).Select(x => x.ToString());
        return $"You gain proficiency with the following tools: {string.Join(", ", list)}";
    }
}
