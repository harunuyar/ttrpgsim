namespace DnD.Entities.Items.Equipments.Armors.LightArmor;

using DnD.Entities.Characters;
using DnD.Entities.Items.Equipments.Armors;
using DnD.Entities.Units;

internal class LeatherArmor : IArmor
{
    public LeatherArmor()
        : base(
            EArmorType.Light,
            "Leather Armor",
            "Leather armor is made of soft, supple leather.",
            Weight.OfPounds(10),
            Worth.OfGold(10),
            0,
            false)
    {
    }

    public override int GetArmorClass(Character character)
    {
        return 11 + GetDexterityModifier(character);
    }
}
