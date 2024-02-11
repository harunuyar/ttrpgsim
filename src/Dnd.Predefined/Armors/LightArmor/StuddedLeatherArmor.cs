namespace Dnd.Predefined.Armors.LightArmor;

using Dnd.System.Entities.Characters;
using Dnd.System.Entities.Items.Equipments.Armors;
using Dnd.System.Entities.Units;

public class StuddedLeatherArmor : AArmor
{
    public static StuddedLeatherArmor Instance { get; } = new StuddedLeatherArmor();

    private StuddedLeatherArmor()
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

    public override int GetDexterityBonus(ICharacter character)
    {
        return GetDexterityModifier(character);
    }
}
