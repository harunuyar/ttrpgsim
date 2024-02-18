namespace Dnd.Predefined.Feats.Classes.Fighter;

using Dnd.Predefined.Actions.Classes.Fighter;
using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.CommandSystem.Commands.ListCommands;

public class Interception : AFeat
{
    public Interception() : base("Interception", "When a creature you can see hits a target, other than you, within 5 feet of you with an attack, you can use your reaction to reduce the damage the target takes by 1d10 + your proficiency bonus (to a minimum of 0 damage). You must be wielding a shield or a simple or martial weapon to use this reaction.")
    {
        Intercept = new Intercept();
    }

    public Intercept Intercept { get; }

    public override void HandleCommand(ICommand command)
    {
        if (command is GetActions getActions)
        {
            getActions.AddItem(this, Intercept);
        }
    }
}
