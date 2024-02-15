namespace Dnd.Predefined.Items.Tools;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Items.Tools;
using Dnd.System.Entities.Units;

public abstract class ATool : ITool
{
    public ATool(string name, string description, EToolType toolType, Weight weight, Value value)
    {
        Name = name;
        Description = description;
        Weight = weight;
        Value = value;
        ToolType = toolType;
    }

    public string Name { get; }

    public string Description { get; }

    public Weight Weight { get; }

    public Value Value { get; }

    public bool IsStackable => true;

    public bool IsConsumable => true;

    public bool IsEquippable => false;

    public EToolType ToolType { get; }

    public virtual void HandleCommand(ICommand command)
    {
    }
}
