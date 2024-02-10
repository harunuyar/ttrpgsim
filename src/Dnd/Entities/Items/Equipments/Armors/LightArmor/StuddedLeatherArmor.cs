namespace Dnd.Entities.Items.Equipments.Armors.LightArmor;

using Dnd.Entities.Characters;
using Dnd.Entities.Items.Equipments.Armors;
using Dnd.Entities.Units;

public class StuddedLeatherArmor : AArmor
{
    public StuddedLeatherArmor()
        : base(
            EArmorType.Light,
            "Studded Leather Armor",
            "Studded leather armor is made of tough but flexible leather, with close-set rivets to add additional protection.",
            12,
            Weight.OfPounds(13),
            Value.OfGold(45),
            0,
            false)
    {
    }

    public override int GetDexterityBonus(Character character)
    {
        return GetDexterityModifier(character);
    }
}
