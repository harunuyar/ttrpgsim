namespace Dnd.Predefined.Items.Armors.Shield;

using Dnd.Predefined.Items.Armors;

using Dnd.System.Entities.Units;

public class Shield : AShield
{
    public static Shield Instance { get; } = new Shield();

    private Shield()
        : base("Shield",
            "A shield is made from wood or metal and is carried in one hand. Wielding a shield increases your Armor Class by 2. You can benefit from only one shield at a time.",
            Weight.OfPounds(6),
            Value.OfGold(10),
            2)
    {
    }
}
