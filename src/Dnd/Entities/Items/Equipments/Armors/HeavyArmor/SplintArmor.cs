namespace Dnd.Entities.Items.Equipments.Armors.HeavyArmor;

using Dnd.Entities.Characters;
using Dnd.Entities.Items.Equipments.Armors;
using Dnd.Entities.Units;

public class SplintArmor : AArmor
{
    public SplintArmor()
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

    public override int GetDexterityBonus(Character character)
    {
        return 0;
    }
}
