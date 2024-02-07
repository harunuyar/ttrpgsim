namespace DnD.CommandSystem.Commands;

using DnD.CommandSystem.CommandResult;
using DnD.Entities.Attributes;
using DnD.Entities.Characters;

internal class GetAttributeModifierCommand : ICommand
{
    public GetAttributeModifierCommand(Player player, EAttributeType attributeType)
    {
        Player = player;
        AttributeType = attributeType;
    }

    public Player Player { get; }
    public EAttributeType AttributeType { get; }

    public ICommandResult Execute()
    {
        try
        {
            return IntegerResult.Success(this, Player.AttributeSet.GetAttribute(AttributeType).GetModifier());
        }
        catch (Exception e)
        {
            return IntegerResult.Failure(this, e.Message);
        }
    }
}
