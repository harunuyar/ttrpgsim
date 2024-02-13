namespace Dnd.Predefined.Feats.Classes.Fighter.Level2;

using Dnd.System.CommandSystem.Commands;

public class ActionSurge : AFeat
{
    public ActionSurge() : base("Action Surge", "On your turn, you can take one additional action. Once per short/long rest.")
    {
    }

    public override void HandleCommand(ICommand command)
    {
        // TODO: Add Action Surge to GetAvailableActions command with one charge per short/long rest
    }
}
