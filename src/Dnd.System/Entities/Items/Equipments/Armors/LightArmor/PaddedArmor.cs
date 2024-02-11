namespace Dnd.Entities.Items.Equipments.Armors.LightArmor;

using Dnd.Entities.Characters;
using Dnd.Entities.Items.Equipments.Armors;
using Dnd.Entities.Units;

public class PaddedArmor : AArmor
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
