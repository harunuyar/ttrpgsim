namespace DnD.Entities.Items.Equipments.Armors.HeavyArmor;

using DnD.Entities.Characters;
using DnD.Entities.Items.Equipments.Armors;
using DnD.Entities.Units;

internal class RingMailArmor : IArmor
{
    public RingMailArmor()
        : base(
            EArmorType.Heavy,
            "Ring Mail Armor",
            "Ring mail is old-fashioned and looks like it belongs in a museum. It is made of interlocking metal rings. It includes a heavy cloth undergarment. The armor has a coif and a mantle, which covers the shoulders and the neck. The armor includes gauntlets.",
            Weight.OfPounds(40),
            Worth.OfGold(30),
            0,
            true)
    {
    }

    public override int GetArmorClass(Character character)
    {
        return 14;
    }
}
