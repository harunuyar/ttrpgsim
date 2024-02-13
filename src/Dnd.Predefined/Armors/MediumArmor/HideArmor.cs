namespace Dnd.Predefined.Armors.MediumArmor;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Items.Equipments.Armors;
using Dnd.System.Entities.Units;

public class HideArmor : AArmor
{
    public static HideArmor Instance { get; } = new HideArmor();

    private HideArmor()
        : base(
            EArmorType.Medium,
            "Hide Armor",
            "Hide armor is made from thick and tough animal hides.",
            12,
            Weight.OfPounds(12),
            Value.OfGold(10),
            0,
            false)
    {
    }

    public override int GetDexterityBonus(IGameActor character)
    {
        return Math.Min(GetDexterityModifier(character), 2);
    }
}
