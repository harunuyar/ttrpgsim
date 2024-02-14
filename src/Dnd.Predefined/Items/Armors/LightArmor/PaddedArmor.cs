namespace Dnd.Predefined.Items.Armors.LightArmor;

using Dnd.Predefined.Items.Armors;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Items.Equipments.Armors;
using Dnd.System.Entities.Units;

public class PaddedArmor : AArmor
{
    public static PaddedArmor Instance { get; } = new PaddedArmor();

    private PaddedArmor()
        : base(
            EArmorType.Light,
            "Padded Armor",
            "Padded armor consists of quilted layers of cloth and batting.",
            11,
            Weight.OfPounds(8),
            Value.OfGold(5),
            0,
            true)
    {
    }

    public override int GetDexterityBonus(IGameActor character)
    {
        return GetDexterityModifier(character);
    }
}
