namespace Dnd.Predefined.Items.Armors.MediumArmor;

using Dnd.Predefined.Items.Armors;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Items.Equipments.Armors;
using Dnd.System.Entities.Units;

public class ScaleMailArmor : AArmor
{
    public static ScaleMailArmor Instance { get; } = new ScaleMailArmor();

    private ScaleMailArmor()
        : base(
            EArmorType.Medium,
            "Scale Mail Armor",
            "Scale mail consists of a shirt and leggings made of small metal scales affixed to a leather backing. The shirt includes a layer of quilted fabric underneath the scales.",
            14,
            Weight.OfPounds(45),
            Value.OfGold(50),
            0,
            true)
    {
    }

    public override int GetDexterityBonus(IGameActor character)
    {
        return Math.Min(GetDexterityModifier(character), 2);
    }
}
