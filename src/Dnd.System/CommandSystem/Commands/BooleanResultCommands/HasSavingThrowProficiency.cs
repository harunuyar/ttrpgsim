namespace Dnd.System.CommandSystem.Commands.BooleanResultCommands;

using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.GameActors;

public class HasSavingThrowProficiency : DndBooleanCommand
{
    public HasSavingThrowProficiency(IGameActor character, EAttributeType attributeType) : base(character)
    {
        AttributeType = attributeType;
    }

    public EAttributeType AttributeType { get; }

    protected override void InitializeResult()
    {
        Result.SetValue("Base", false);
    }
}
