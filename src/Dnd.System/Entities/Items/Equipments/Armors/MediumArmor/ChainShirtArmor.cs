namespace Dnd.Entities.Items.Equipments.Armors.MediumArmor;

using Dnd.Entities.Characters;
using Dnd.Entities.Items.Equipments.Armors;
using Dnd.Entities.Units;

public class ChainShirtArmor : AArmor
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
