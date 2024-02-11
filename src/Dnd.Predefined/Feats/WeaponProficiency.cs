namespace Dnd.Predefined.Feats;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.CommandSystem.Commands.BooleanResultCommands;
using Dnd.System.Entities.Items.Equipments.Weapons;

public class WeaponProficiency : AFeat
{
    public WeaponProficiency(EWeaponType weaponType) : base("Weapon Proficiency", GetDescription(weaponType))
    {
        WeaponType = weaponType;
    }

    public EWeaponType WeaponType { get; }

    public override void HandleCommand(DndCommand command)
    {
        if (command is HasWeaponProficiency hasWeaponProficiency && WeaponType.HasFlag(hasWeaponProficiency.WeaponType))
        {
            hasWeaponProficiency.Result.SetValue(this, true);
        }
    }

    private static string GetDescription(EWeaponType weaponType)
    {
        var list = new List<string>();

        if (weaponType.HasFlag(EWeaponType.All))
        {
            list.Add("All");
        }
        else
        {
            if (weaponType.HasFlag(EWeaponType.SimpleWeapon))
            {
                list.Add("Simple Weapon");
            }
            else
            {
                if (weaponType.HasFlag(EWeaponType.SimpleMeleeWeapon))
                {
                    list.Add("Simple Melee Weapon");
                }
                else
                {
                    list.AddRange(new[] { 
                            EWeaponType.Club, EWeaponType.Dagger, EWeaponType.Greatclub, EWeaponType.Handaxe, EWeaponType.Javelin, 
                            EWeaponType.LightHammer, EWeaponType.Mace, EWeaponType.Quarterstaff, EWeaponType.Sickle, EWeaponType.Spear }
                        .Where(wt => weaponType.HasFlag(wt))
                        .Select(wt => wt.ToString()));
                }
                
                if (weaponType.HasFlag(EWeaponType.SimpleRangedWeapon))
                {
                    list.Add("Simple Ranged Weapon");
                }
                else
                {
                    list.AddRange(new[] { EWeaponType.CrossbowLight, EWeaponType.Dart, EWeaponType.Shortbow, EWeaponType.Sling }
                        .Where(wt => weaponType.HasFlag(wt))
                        .Select(wt => wt.ToString()));
                }
            }

            if (weaponType.HasFlag(EWeaponType.MartialWeapon))
            {
                list.Add("Martial Weapon");
            }
            else
            {
                if (weaponType.HasFlag(EWeaponType.MartialMeleeWeapon))
                {
                    list.Add("Martial Melee Weapon");
                }
                else
                {
                    list.AddRange(new[] { 
                            EWeaponType.Battleaxe, EWeaponType.Flail, EWeaponType.Glaive, EWeaponType.Greataxe, EWeaponType.Greatsword, EWeaponType.Halberd, 
                            EWeaponType.Lance, EWeaponType.Longsword, EWeaponType.Maul, EWeaponType.Morningstar, EWeaponType.Pike, EWeaponType.Rapier, EWeaponType.Scimitar, 
                            EWeaponType.Shortsword, EWeaponType.Trident, EWeaponType.WarPick, EWeaponType.Warhammer, EWeaponType.Whip }
                        .Where(wt => weaponType.HasFlag(wt))
                        .Select(wt => wt.ToString()));
                }

                if (weaponType.HasFlag(EWeaponType.MartialRangedWeapon))
                {
                    list.Add("Martial Ranged Weapon");
                }
                else
                {
                    list.AddRange(new[] { EWeaponType.Blowgun, EWeaponType.CrossbowHand, EWeaponType.CrossbowHeavy, EWeaponType.Longbow, EWeaponType.Net }
                        .Where(wt => weaponType.HasFlag(wt))
                        .Select(wt => wt.ToString()));
                }
            }
        }

        return "You gain proficiency with the following weapons: " + string.Join(", ", list);
    }
}
