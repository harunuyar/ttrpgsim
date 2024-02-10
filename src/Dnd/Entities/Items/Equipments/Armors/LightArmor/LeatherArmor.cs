namespace Dnd.Entities.Items.Equipments.Armors.LightArmor;

using Dnd.Entities.Characters;
using Dnd.Entities.Items.Equipments.Armors;
using Dnd.Entities.Units;

public class LeatherArmor : AArmor
{
    public LeatherArmor()
        : base(
            EArmorType.Light,
            "Leather Armor",
            "Leather armor is made of soft, supple leather.",
            11,
            Weight.OfPounds(10),
            Value.OfGold(10),
            0,
            false)
    {
    }

    public override int GetDexterityBonus(Character character)
    {
        return GetDexterityModifier(character);
    }
}
