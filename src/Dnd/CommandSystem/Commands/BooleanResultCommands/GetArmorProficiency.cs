namespace Dnd.CommandSystem.Commands.BooleanResultCommands;

using Dnd.Entities.Characters;
using Dnd.Entities.Items.Equipments.Armors;

public class GetArmorProficiency : DndBooleanCommand
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
