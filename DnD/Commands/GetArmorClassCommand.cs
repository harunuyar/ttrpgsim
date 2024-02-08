namespace DnD.Commands;

using DnD.Entities.Attributes;
using DnD.Entities.Characters;
using DnD.Entities.Items.Equipments.Armors;
using TableTopRpg.Commands;

internal class GetArmorClassCommand : DndCommand
{
    public GetArmorClassCommand(Character character) : base(character)
    {
    }

    protected override ICommandResult ExecuteDndCommand()
    {
        if (Character.Inventory.Items.Find(i => i.IsEquipped && i is IArmor) is IArmor armor)
        {
            return SummarizedIntegerResult.Success(this, armor.GetArmorClass(Character), armor.Name);
        }

        ICommand command = new GetAttributeModifierCommand(Character, EAttributeType.Dexterity);
        ICommandResult result = command.Execute();

        int dexterityModifier = 0;

        if (result.IsSuccess && result is IntegerResult integerResult)
        {
            dexterityModifier = integerResult.Value;
        }

        var summarizedIntegerResult = SummarizedIntegerResult.Success(this, 10, "Base");
        summarizedIntegerResult.BonusValues.Add((dexterityModifier, "Dexterity Modifier"));
        return summarizedIntegerResult;
    }
}
