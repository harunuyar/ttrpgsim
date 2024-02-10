namespace DnD.Entities.Items.Equipments.Armors.LightArmor;

using DnD.Entities.Characters;
using DnD.Entities.Items.Equipments.Armors;
using DnD.Entities.Units;

internal class PaddedArmor : AArmor
{
    public PaddedArmor()
        : base(
            EArmorType.Light,
            "Padded Armor",
            "Padded armor consists of quilted layers of cloth and batting.",
            11,
            Weight.OfPounds(8),
            Value.OfGold(5),
            0,
            true)
    {
    }

    public override int GetDexterityBonus(Character character)
    {
        return GetDexterityModifier(character);
    }
}
