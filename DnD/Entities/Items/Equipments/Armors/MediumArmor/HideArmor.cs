namespace DnD.Entities.Items.Equipments.Armors.MediumArmor;

using DnD.Entities.Characters;
using DnD.Entities.Items.Equipments.Armors;
using DnD.Entities.Units;

internal class HideArmor : IArmor
{
    public HideArmor()
        : base(
            EArmorType.Medium,
            "Hide Armor",
            "Hide armor is made from thick and tough animal hides.",
            12,
            Weight.OfPounds(12),
            Worth.OfGold(10),
            0,
            false)
    {
    }

    public override int GetDexterityBonus(Character character)
    {
        return Math.Min(GetDexterityModifier(character), 2);
    }
}
