namespace DnD.Entities.Classes.Predefined;

using DnD.Entities.Attributes;
using DnD.Entities.Items.Equipments.Armors;
using DnD.Entities.Items.Equipments.Weapons;
using DnD.Entities.Skills;
using DnD.Entities.Skills.Predefined;
using DnD.GameManagers.Dice;
using System.Collections.Generic;

internal class Sorcerer : IDndClass
{
    public EDiceType HitDie => EDiceType.D6;

    public EAttributeType SavingThrowProficiencies => EAttributeType.Constitution | EAttributeType.Charisma;

    public EArmorType ArmorProficiencies => EArmorType.None;

    public EWeaponType WeaponProficiencies => EWeaponType.Dagger | EWeaponType.Dart | EWeaponType.Sling | EWeaponType.Quarterstaff | EWeaponType.CrossbowLight;

    public string Name => "Sorcerer";

    public string Description => "A spellcaster who draws on inherent magic from a gift or bloodline";

    public List<IDndSkill> ChoosableSkillProficiencies => new List<IDndSkill>()
    {
        Arcana.Instance,
        Deception.Instance,
        Insight.Instance,
        Intimidation.Instance,
        Persuasion.Instance,
        Religion.Instance
    };

    public int NumberOfSkillProficiencies => 2;

    private Sorcerer() { }

    public static readonly Sorcerer Instance = new Sorcerer();
}
