namespace Dnd.Entities.Items.Equipments.Armors.MediumArmor;

using Dnd.Entities.Characters;
using Dnd.Entities.Items.Equipments.Armors;
using Dnd.Entities.Units;

public class BreastplateArmor : AArmor
{
    public BreastplateArmor()
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

    public override int GetDexterityBonus(Character character)
    {
        return Math.Min(GetDexterityModifier(character), 2);
    }
}
