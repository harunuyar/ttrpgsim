namespace Dnd.Predefined.Feats.Classes.Fighter;

using Dnd.Predefined.Actions.Classes.Fighter;
using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.CommandSystem.Commands.ListCommands;

public class Indomitable : AFeat
{
    public static readonly int[] ChargesPerLevel = { 1, 2, 3 };

    public Indomitable(int level) : base("Indomitable", "You can reroll a saving throw that you fail. If you do so, you must use the new roll, and you can’t use this feature again until you finish a long rest.")
    {
        Level = level;
        IndomitableReroll = new IndomitableReroll(ChargesPerLevel[level - 1]);
    }

    public int Level { get; }

    public IndomitableReroll IndomitableReroll { get; }

    public override void HandleCommand(ICommand command)
    {
        if (command is GetActions getActions)
        {
            getActions.AddByOverriding(this, [IndomitableReroll], (a, b) => a.Level > b.Level);
        }

        IndomitableReroll.HandleCommand(command);
    }
}
