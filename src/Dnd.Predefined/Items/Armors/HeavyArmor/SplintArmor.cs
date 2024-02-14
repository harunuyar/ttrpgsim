namespace Dnd.Predefined.Items.Armors.HeavyArmor;

using Dnd.Predefined.Items.Armors;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Items.Equipments.Armors;
using Dnd.System.Entities.Units;

public class SplintArmor : AArmor
{
    public static SplintArmor Instance { get; } = new SplintArmor();

    private SplintArmor()
        : base(
            EArmorType.Heavy,
            "Splint Armor",
            "Splint armor is made of narrow vertical strips of metal riveted to a backing of leather that is worn over cloth padding. Flexible chain mail protects the joints.",
            17,
            Weight.OfPounds(60),
            Value.OfGold(200),
            15,
            true)
    {
    }

    public override int GetDexterityBonus(IGameActor character)
    {
        return 0;
    }
}
