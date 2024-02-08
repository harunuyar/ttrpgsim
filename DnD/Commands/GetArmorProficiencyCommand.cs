namespace DnD.Commands;

using DnD.Entities.Characters;
using DnD.Entities.Items.Equipments.Armors;
using TableTopRpg.Commands;

internal class GetArmorProficiencyCommand : DndCommand
{
    public GetArmorProficiencyCommand(Character character, EArmorType armorType) : base(character)
    {
        ArmorType = armorType;
    }

    public EArmorType ArmorType { get; }

    protected override ICommandResult ExecuteDndCommand()
    {
        return BoolResult.Success(this, Character.GetArmorProficiency(ArmorType));
    }
}
