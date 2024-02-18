namespace Dnd.Predefined.Feats.Classes.Fighter;

using Dnd.Predefined.Actions.Classes.Fighter;
using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.CommandSystem.Commands.ListCommands;

public class SecondWind : AFeat
{
    
    public SecondWind() : base("Second Wind", "You can use a bonus action to regain hit points equal to 1d10 + your fighter level. Once per short/long rest.")
    {
        SecondWindAction = new SecondWindAction();
    }

    public SecondWindAction SecondWindAction { get; }

    public override void HandleCommand(ICommand command)
    {
        if (command is GetActions getPossibleActions)
        {
            getPossibleActions.AddItem(this, SecondWindAction);
        }

        SecondWindAction.HandleCommand(command);
    }
}
