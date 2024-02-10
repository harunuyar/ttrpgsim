namespace DnD.Entities.Items.Equipments.Armors.HeavyArmor;

using DnD.Entities.Characters;
using DnD.Entities.Items.Equipments.Armors;
using DnD.Entities.Units;

internal class RingMailArmor : AArmor
{
    public RingMailArmor()
        : base(
            EArmorType.Heavy,
            "Ring Mail Armor",
            "Ring mail is old-fashioned and looks like it belongs in a museum. It is made of interlocking metal rings. It includes a heavy cloth undergarment. The armor has a coif and a mantle, which covers the shoulders and the neck. The armor includes gauntlets.",
            14,
            Weight.OfPounds(40),
            Value.OfGold(30),
            0,
            true)
    {
    }

    public override int GetDexterityBonus(Character character)
    {
        return 0;
    }
}
