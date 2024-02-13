namespace Dnd.Predefined.Armors.HeavyArmor;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Items.Equipments.Armors;
using Dnd.System.Entities.Units;

public class ChainMailArmor : AArmor
{
    public static ChainMailArmor Instance { get; } = new ChainMailArmor();

    private ChainMailArmor()
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

    public override int GetDexterityBonus(IGameActor character)
    {
        return 0;
    }
}
