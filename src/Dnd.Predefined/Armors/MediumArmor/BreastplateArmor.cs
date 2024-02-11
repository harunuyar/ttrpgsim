namespace Dnd.Predefined.Armors.MediumArmor;

using Dnd.System.Entities.Characters;
using Dnd.System.Entities.Items.Equipments.Armors;
using Dnd.System.Entities.Units;

public class BreastplateArmor : AArmor
{
    public static BreastplateArmor Instance { get; } = new BreastplateArmor();

    private BreastplateArmor()
        : base(
            EArmorType.Medium,
            "Breastplate Armor",
            "Breastplate consists of a fitted metal chest piece worn with supple leather. Although it leaves the legs and arms relatively unprotected, this armor provides good protection for the wearer's vital organs while leaving the wearer relatively unencumbered.",
            14,
            Weight.OfPounds(20),
            Value.OfGold(400),
            0,
            false)
    {
    }

    public override int GetDexterityBonus(ICharacter character)
    {
        return Math.Min(GetDexterityModifier(character), 2);
    }
}
