namespace Dnd.Predefined.Armors.MediumArmor;

using Dnd.System.Entities.Characters;
using Dnd.System.Entities.Items.Equipments.Armors;
using Dnd.System.Entities.Units;

public class ChainShirtArmor : AArmor
{
    public static ChainShirtArmor Instance { get; } = new ChainShirtArmor();

    private ChainShirtArmor()
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

    public override int GetDexterityBonus(ICharacter character)
    {
        return Math.Min(GetDexterityModifier(character), 2);
    }
}
