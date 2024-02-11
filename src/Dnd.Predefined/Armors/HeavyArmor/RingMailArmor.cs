namespace Dnd.Predefined.Armors.HeavyArmor;

using Dnd.System.Entities.Characters;
using Dnd.System.Entities.Items.Equipments.Armors;
using Dnd.System.Entities.Units;

public class RingMailArmor : AArmor
{
    public static RingMailArmor Instance { get; } = new RingMailArmor();

    private RingMailArmor()
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

    public override int GetDexterityBonus(ICharacter character)
    {
        return 0;
    }
}
