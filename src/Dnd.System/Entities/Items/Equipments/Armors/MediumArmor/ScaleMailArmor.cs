namespace Dnd.Entities.Items.Equipments.Armors.MediumArmor;

using Dnd.Entities.Characters;
using Dnd.Entities.Items.Equipments.Armors;
using Dnd.Entities.Units;

public class ScaleMailArmor : AArmor
{
    public ScaleMailArmor()
        : base(
            EArmorType.Medium,
            "Scale Mail Armor",
            "Scale mail consists of a shirt and leggings made of small metal scales affixed to a leather backing. The shirt includes a layer of quilted fabric underneath the scales.",
            14,
            Weight.OfPounds(45),
            Value.OfGold(50),
            0,
            true)
    {
    }

    public override int GetDexterityBonus(Character character)
    {
        return Math.Min(GetDexterityModifier(character), 2);
    }
}
