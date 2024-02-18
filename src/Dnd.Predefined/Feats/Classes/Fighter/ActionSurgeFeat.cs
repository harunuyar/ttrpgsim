namespace Dnd.Predefined.Feats.Classes.Fighter;

using Dnd.Predefined.Actions.Classes.Fighter;
using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.CommandSystem.Commands.ListCommands;

public class ActionSurgeFeat : AFeat
{
    public ActionSurgeFeat(int level) : base("Action Surge", $"On your turn, you can take one additional action. {level} times per short/long rest.")
    {
        Level = level;
        ActionSurge = new ActionSurge(level);
    }

    public int Level { get; }

    public ActionSurge ActionSurge { get; }

    public override void HandleCommand(ICommand command)
    {
        if (command is GetActions getPossibleActions)
        {
            getPossibleActions.AddByOverriding(this, [ActionSurge], (a, b) => a.Level > b.Level);
        }

        ActionSurge.HandleCommand(command);
    }
}
