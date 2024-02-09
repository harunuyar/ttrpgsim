namespace DnD.CommandSystem.Commands.BooleanResultCommands;

using DnD.CommandSystem.Results;
using DnD.Entities.Characters;
using DnD.Entities.Items.Equipments.Armors;

internal class GetArmorProficiency : DndBooleanCommand
{
    public GetArmorProficiency(Character character, EArmorType armorType) : base(character)
    {
        ArmorType = armorType;
    }

    public EArmorType ArmorType { get; }

    public override BooleanResultWithBonuses Execute()
    {
        return BooleanResultWithBonuses.Success(this, "Base Armor Proficiency", Character.HasArmorProficiency(ArmorType), BooleanBonuses);
    }
}
