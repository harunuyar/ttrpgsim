namespace Dnd.Predefined.Items.Wizard;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.Entities.Items;
using Dnd.System.Entities.Units;

public class SpellBookDescription : IItemDescription
{
    public static readonly SpellBookDescription Instance = new SpellBookDescription();

    public string Name => "Spellbook";

    public string Description => "A spellbook is a leather-bound tome with 100 blank vellum pages suitable for recording spells.";

    public bool IsStackable => false;

    public bool IsConsumable => false;

    public bool IsEquippable => false;

    public Weight Weight => Weight.OfPounds(3);

    public Value Value => Value.OfGold(50);

    public void HandleCommand(ICommand command)
    {
    }
}
