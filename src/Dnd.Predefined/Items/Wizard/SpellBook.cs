namespace Dnd.Predefined.Items.Wizard;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.Entities.Items;
using Dnd.System.Entities.Spells;

public class SpellBook : AItem
{
    public SpellBook() : base(SpellBookDescription.Instance)
    {
        Spells = new List<ISpell>();
    }

    public List<ISpell> Spells { get; }

    public override void HandleCommand(ICommand command)
    {
    }
}
