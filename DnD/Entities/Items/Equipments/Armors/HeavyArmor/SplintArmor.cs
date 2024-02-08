namespace DnD.Entities.Items.Equipments.Armors.HeavyArmor;

using DnD.Entities.Characters;
using DnD.Entities.Items.Equipments.Armors;
using DnD.Entities.Units;

internal class SplintArmor : IArmor
{
    public SplintArmor()
        : base(
            EArmorType.Heavy,
            "Splint Armor",
            "Splint armor is made of narrow vertical strips of metal riveted to a backing of leather that is worn over cloth padding. Flexible chain mail protects the joints.",
            Weight.OfPounds(60),
            Worth.OfGold(200),
            15,
            true)
    {
    }

    public override int GetArmorClass(Character character)
    {
        return 17;
    }
}
