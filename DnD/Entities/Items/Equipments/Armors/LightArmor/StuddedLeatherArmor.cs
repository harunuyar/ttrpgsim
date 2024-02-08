namespace DnD.Entities.Items.Equipments.Armors.LightArmor;

using DnD.Entities.Characters;
using DnD.Entities.Items.Equipments.Armors;
using DnD.Entities.Units;

internal class StuddedLeatherArmor : IArmor
{
    public StuddedLeatherArmor()
        : base(
            EArmorType.Light,
            "Studded Leather Armor",
            "Studded leather armor is made of tough but flexible leather, with close-set rivets to add additional protection.",
            Weight.OfPounds(13),
            Worth.OfGold(45),
            0,
            false)
    {
    }

    public override int GetArmorClass(Character character)
    {
        return 12 + GetDexterityModifier(character);
    }
}
