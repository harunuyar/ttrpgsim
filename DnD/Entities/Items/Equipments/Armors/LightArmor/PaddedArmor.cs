namespace DnD.Entities.Items.Equipments.Armors.LightArmor;

using DnD.Entities.Characters;
using DnD.Entities.Items.Equipments.Armors;
using DnD.Entities.Units;

internal class PaddedArmor : IArmor
{
    public PaddedArmor()
        : base(
            EArmorType.Light,
            "Padded Armor",
            "Padded armor consists of quilted layers of cloth and batting.",
            Weight.OfPounds(8),
            Worth.OfGold(5),
            0,
            true)
    {
    }

    public override int GetArmorClass(Character character)
    {
        return 11 + GetDexterityModifier(character);
    }
}
