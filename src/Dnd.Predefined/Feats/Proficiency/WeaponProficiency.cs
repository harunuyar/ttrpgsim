namespace Dnd.Predefined.Feats.Proficiency;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.CommandSystem.Commands.BooleanResultCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
using Dnd.System.Entities.Items.Equipments.Weapons;

public class WeaponProficiency : AFeat
{
    public WeaponProficiency(EWeaponType weaponType) : base("Weapon Proficiency", GetDescription(weaponType))
    {
        WeaponType = weaponType;
    }

    public EWeaponType WeaponType { get; }

    public override void HandleCommand(ICommand command)
    {
        if (command is HasWeaponProficiency hasWeaponProficiency)
        {
            if (WeaponType.HasFlag(hasWeaponProficiency.WeaponType))
            {
                hasWeaponProficiency.SetValue(this, true, $"You have {hasWeaponProficiency.WeaponType} proficiency.");
            }
        }
        else if (command is GetWeaponAttackModifier calculateWeaponAttackModifier)
        {
            if (calculateWeaponAttackModifier.WeaponItem.ItemDescription is IWeapon weapon && WeaponType.HasFlag(weapon.WeaponType))
            {
                var getProficiencyBonus = new GetProficiencyBonus(command.Actor);
                var proficiencyBonus = getProficiencyBonus.Execute();

                if (!proficiencyBonus.IsSuccess)
                {
                    calculateWeaponAttackModifier.SetErrorAndReturn("GetProficiencyBonus: " + proficiencyBonus.ErrorMessage);
                    return;
                }

                calculateWeaponAttackModifier.AddBonus(this, proficiencyBonus.Value);
            }
        }
        else if (command is GetWeaponSavingDifficultyClass calculateWeaponSavingDC)
        {
            if (calculateWeaponSavingDC.WeaponItem.ItemDescription is IWeapon weapon && WeaponType.HasFlag(weapon.WeaponType))
            {
                var getProficiencyBonus = new GetProficiencyBonus(command.Actor);
                var proficiencyBonus = getProficiencyBonus.Execute();

                if (!proficiencyBonus.IsSuccess)
                {
                    calculateWeaponSavingDC.SetErrorAndReturn("GetProficiencyBonus: " + proficiencyBonus.ErrorMessage);
                    return;
                }

                calculateWeaponSavingDC.AddBonus(this, proficiencyBonus.Value);
            }
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
