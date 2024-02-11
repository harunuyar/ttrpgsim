namespace Dnd.Predefined.Armors.LightArmor;

using Dnd.System.Entities.Characters;
using Dnd.System.Entities.Items.Equipments.Armors;
using Dnd.System.Entities.Units;

public class LeatherArmor : AArmor
{
    public static LeatherArmor Instance { get; } = new LeatherArmor();

    private LeatherArmor()
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

    public override int GetDexterityBonus(ICharacter character)
    {
        return GetDexterityModifier(character);
    }
}
