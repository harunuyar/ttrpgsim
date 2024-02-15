namespace Dnd.System.Entities.Items.Tools;

public interface ITool : IItemDescription
{
    EToolType ToolType { get; }
}
