namespace Dnd.Predefined.Feats.Classes.Fighter.Level1.FightingStyle;

using Dnd.System.CommandSystem.Commands;

public class Protection : AFeat, IFightingStyle
{
    public Protection() : base("Protection", "When a creature you can see attacks a target other than you that is within 5 feet of you, you can use your reaction to impose disadvantage on the attack roll. You must be wielding a shield.")
    {
    }

    public override void HandleCommand(ICommand command)
    {
        // TODO: add Protection to GetAvailableActions command with unlimited uses
    }
}
