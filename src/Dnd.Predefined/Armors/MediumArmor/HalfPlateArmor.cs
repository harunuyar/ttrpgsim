namespace Dnd.Predefined.Armors.MediumArmor;

using Dnd.System.Entities.Characters;
using Dnd.System.Entities.Items.Equipments.Armors;
using Dnd.System.Entities.Units;

public class HalfPlateArmor : AArmor
{
    public static HalfPlateArmor Instance { get; } = new HalfPlateArmor();

    private HalfPlateArmor()
        : base(
            EArmorType.Medium,
            "Half Plate Armor",
            "Half plate consists of shaped metal plates that cover most of the wearer's body. It does not include leg protection beyond simple greaves that are attached with leather straps. The breastplate and shoulder protectors of half plate are made of interlocking pieces of metal, while the back and waist are made of a single piece of metal. The arm pieces consist of separate metal pieces layered over a leather base. The arm pieces are strapped to the arm and shoulder. The leg pieces consist of metal shin and thigh guards strapped to the leg. The suit includes gauntlets.",
            15,
            Weight.OfPounds(40),
            Value.OfGold(750),
            0,
            true)
    {
    }

    public override int GetDexterityBonus(ICharacter character)
    {
        return Math.Min(GetDexterityModifier(character), 2);
    }
}
