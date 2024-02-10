namespace DnD.CommandSystem.Commands.BooleanResultCommands;

using DnD.Entities.Characters;
using DnD.Entities.Items.Equipments.Armors;

internal class GetArmorProficiency : DndBooleanCommand
{
    public GetArmorProficiency(Character character, EArmorType armorType) : base(character)
    {
        ArmorType = armorType;
    }

    public EArmorType ArmorType { get; }

    public override void InitializeResult()
    {
        Result.SetValue("Base Armor Proficiency", Character.HasArmorProficiency(ArmorType));
    }
}
