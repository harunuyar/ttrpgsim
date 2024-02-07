namespace DnD.CommandSystem.Commands;

using DnD.CommandSystem.CommandResult;
using DnD.Entities.Characters;

internal class GetInitiativeModifier : ICommand
{
    public GetInitiativeModifier(Player player)
    {
        Player = player;
    }

    public Player Player { get; }

    public ICommandResult Execute()
    {
        return IntegerResult.Success(this, Player.AttributeSet.Dexterity.GetModifier());
    }
}
