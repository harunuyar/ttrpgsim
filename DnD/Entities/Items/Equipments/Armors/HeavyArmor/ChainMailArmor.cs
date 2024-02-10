namespace DnD.Entities.Items.Equipments.Armors.HeavyArmor;

using DnD.Entities.Characters;
using DnD.Entities.Items.Equipments.Armors;
using DnD.Entities.Units;

internal class ChainMailArmor : AArmor
{
    public ChainMailArmor()
        : base(
            EArmorType.Heavy,
            "Chain Mail Armor",
            "Chain mail is made of interlocking metal rings. It includes a layer of quilted fabric underneath the mail to prevent chafing and to cushion the impact of blows. The suit includes gauntlets.",
            16,
            Weight.OfPounds(55),
            Value.OfGold(75),
            13,
            true)
    {
    }

    public override int GetDexterityBonus(Character character)
    {
        return 0;
    }
}
