namespace Dnd.System.Entities.Items.Equipments.Weapons;

public static class EWeaponTypeExtensions
{
    public static bool IsMelee(this EWeaponType weaponType)
    {
        return (weaponType & (EWeaponType.SimpleMeleeWeapon | EWeaponType.MartialMeleeWeapon)) != 0;
    }

    public static bool IsRanged(this EWeaponType weaponType)
    {
        return (weaponType & (EWeaponType.SimpleRangedWeapon | EWeaponType.MartialRangedWeapon)) != 0;
    }
}
