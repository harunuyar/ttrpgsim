namespace Dnd.Predefined.Armors.HeavyArmor;

using Dnd.System.Entities.Characters;
using Dnd.System.Entities.Items.Equipments.Armors;
using Dnd.System.Entities.Units;

public class PlateArmor : AArmor
{
    public static PlateArmor Instance { get; } = new PlateArmor();

    private PlateArmor()
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

    public override int GetDexterityBonus(ICharacter character)
    {
        return 0;
    }
}
