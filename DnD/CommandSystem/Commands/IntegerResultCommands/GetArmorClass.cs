namespace DnD.CommandSystem.Commands.IntegerResultCommands;

using DnD.CommandSystem.Results;
using DnD.Entities.Attributes;
using DnD.Entities.Characters;
using DnD.Entities.Items.Equipments.Armors;

internal class GetArmorClass : DndScoreCommand
{
    public GetArmorClass(Character character) : base(character)
    {
    }

    public override void CollectBonuses()
    {
        if (Character.Inventory.Items.Find(i => i.IsEquipped && i is IArmor) is IArmor armor)
        {
            IntegerBonuses.AddBonus("Dexterity", armor.GetDexterityBonus(Character));
        }
        else
        {
            var command = new GetAttributeModifier(Character, EAttributeType.Dexterity);
            command.CollectBonuses();
            var result = command.Execute();

            if (result.IsSuccess)
            {
                IntegerBonuses.AddBonus("Dexterity", result.Value);
            }
        }

        base.CollectBonuses();
    }

    public override IntegerResultWithBonuses Execute()
    {
        if (Character.Inventory.Items.Find(i => i.IsEquipped && i is IArmor) is IArmor armor)
        {
            return IntegerResultWithBonuses.Success(this, armor.Name, armor.BaseArmorClass, IntegerBonuses);
        }

        return IntegerResultWithBonuses.Success(this, "Base Armor Class", 10, IntegerBonuses);
    }
}
