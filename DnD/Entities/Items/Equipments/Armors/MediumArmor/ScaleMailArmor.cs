namespace DnD.Entities.Items.Equipments.Armors.MediumArmor;

using DnD.Entities.Characters;
using DnD.Entities.Items.Equipments.Armors;
using DnD.Entities.Units;

internal class ScaleMailArmor : IArmor
{
    public ScaleMailArmor()
        : base(
            EArmorType.Medium,
            "Scale Mail Armor",
            "Scale mail consists of a shirt and leggings made of small metal scales affixed to a leather backing. The shirt includes a layer of quilted fabric underneath the scales.",
            14,
            Weight.OfPounds(45),
            Worth.OfGold(50),
            0,
            true)
    {
    }

    public override int GetDexterityBonus(Character character)
    {
        return Math.Min(GetDexterityModifier(character), 2);
    }
}
