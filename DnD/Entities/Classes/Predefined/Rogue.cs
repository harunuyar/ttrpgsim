namespace DnD.Entities.Classes.Predefined;

using DnD.Entities.Attributes;
using DnD.Entities.Items.Equipments.Armors;
using DnD.Entities.Items.Equipments.Weapons;
using DnD.Entities.Skills;
using DnD.Entities.Skills.Predefined;
using DnD.GameManagers.Dice;
using System.Collections.Generic;

internal class Rogue : IDndClass
{
    public EDiceType HitDie => EDiceType.D8;

    public EAttributeType SavingThrowProficiencies => EAttributeType.Dexterity | EAttributeType.Intelligence;

    public EArmorType ArmorProficiencies => EArmorType.Light;

    public EWeaponType WeaponProficiencies => EWeaponType.SimpleWeapon | EWeaponType.CrossbowHand | EWeaponType.Longsword | EWeaponType.Rapier | EWeaponType.Shortsword;

    public string Name => "Rogue";

    public string Description => "A scoundrel who uses stealth and trickery to overcome obstacles and enemies";

    public List<IDndSkill> ChoosableSkillProficiencies => new List<IDndSkill>()
    {
        Acrobatics.Instance,
        Athletics.Instance,
        Deception.Instance,
        Insight.Instance,
        Intimidation.Instance,
        Investigation.Instance,
        Perception.Instance,
        Performance.Instance,
        Persuasion.Instance,
        SleightOfHand.Instance,
        Stealth.Instance
    };

    public int NumberOfSkillProficiencies => 4;

    private Rogue() { }

    public static readonly Rogue Instance = new Rogue();
}
