namespace DnD.Entities.Items.Equipments.Armors.MediumArmor;

using DnD.Entities.Characters;
using DnD.Entities.Items.Equipments.Armors;
using DnD.Entities.Units;

internal class ChainShirtArmor : AArmor
{
    public ChainShirtArmor()
        : base(
            EArmorType.Medium,
            "Chain Shirt Armor",
            "A chain shirt is made of interlocking metal rings, which form a mesh.",
            13,
            Weight.OfPounds(20),
            Value.OfGold(50),
            0,
            false)
    {
    }

    public override int GetDexterityBonus(Character character)
    {
        return Math.Min(GetDexterityModifier(character), 2);
    }
}
