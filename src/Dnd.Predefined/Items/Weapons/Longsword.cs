namespace Dnd.Predefined.Items.Weapons;

using Dnd.GameManagers.Dice;
using Dnd.System.Entities.Damage;
using Dnd.System.Entities.Items.Equipments.Weapons;
using Dnd.System.Entities.Units;

public class Longsword : AWeapon
{
    public Longsword() 
        : base(
            "Longsword", 
            "A longsword", 
            Weight.OfPounds(3), 
            Value.OfGold(15), 
            EWeaponType.Longsword, 
            EDamageCalculationType.Dice, 
            EDamageType.Slashing, 
            EWeaponProperty.Versatile,
            null, 
            new DiceRoll(1, EDiceType.d8), 
            new DiceRoll(1, EDiceType.d10))
    {
    }
}
