namespace Dnd.System.CommandSystem.Commands.BooleanResultCommands;

using Dnd.System.Entities.Characters;
using Dnd.System.Entities.Items.Equipments.Armors;

public class HasArmorProficiency : DndBooleanCommand
{
    public HasArmorProficiency(ICharacter character, EArmorType armorType) : base(character)
    {
        ArmorType = armorType;
    }

    public EArmorType ArmorType { get; }

    public override void InitializeResult()
    {
        Result.SetValue("Default", false);
    }
}
