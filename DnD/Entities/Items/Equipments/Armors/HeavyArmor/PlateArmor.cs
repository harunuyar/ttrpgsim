namespace DnD.Entities.Items.Equipments.Armors.HeavyArmor;

using DnD.Entities.Characters;
using DnD.Entities.Items.Equipments.Armors;
using DnD.Entities.Units;

internal class PlateArmor : AArmor
{
    public PlateArmor()
        : base(
            EArmorType.Heavy,
            "Plate Armor",
            "Plate consists of shaped, interlocking metal plates to cover the entire body. A suit of plate includes gauntlets, heavy leather boots, a visored helmet, and thick layers of padding underneath the armor.",
            18,
            Weight.OfPounds(65),
            Value.OfGold(1500),
            15,
            true)
    {
    }

    public override int GetDexterityBonus(Character character)
    {
        return 0;
    }
}
