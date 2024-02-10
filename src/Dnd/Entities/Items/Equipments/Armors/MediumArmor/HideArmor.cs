namespace Dnd.Entities.Items.Equipments.Armors.MediumArmor;

using Dnd.Entities.Characters;
using Dnd.Entities.Items.Equipments.Armors;
using Dnd.Entities.Units;

public class HideArmor : AArmor
{
    public HideArmor()
        : base(
            EArmorType.Medium,
            "Hide Armor",
            "Hide armor is made from thick and tough animal hides.",
            12,
            Weight.OfPounds(12),
            Value.OfGold(10),
            0,
            false)
    {
    }

    public override int GetDexterityBonus(Character character)
    {
        return Math.Min(GetDexterityModifier(character), 2);
    }
}
