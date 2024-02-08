namespace DnD.Entities.Items.Equipments.Armors.MediumArmor;

using DnD.Entities.Characters;
using DnD.Entities.Items.Equipments.Armors;
using DnD.Entities.Units;

internal class ChainShirtArmor : IArmor
{
    public ChainShirtArmor()
        : base(
            EArmorType.Medium,
            "Chain Shirt Armor",
            "A chain shirt is made of interlocking metal rings, which form a mesh.",
            Weight.OfPounds(20),
            Worth.OfGold(50),
            0,
            false)
    {
    }

    public override int GetArmorClass(Character character)
    {
        return 13 + Math.Min(GetDexterityModifier(character), 2);
    }
}
