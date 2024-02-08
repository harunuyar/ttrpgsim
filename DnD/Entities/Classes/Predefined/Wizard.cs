namespace DnD.Entities.Classes.Predefined;

using DnD.Entities.Attributes;
using DnD.Entities.Items.Equipments.Armors;
using DnD.Entities.Items.Equipments.Weapons;
using DnD.Entities.Skills;
using DnD.Entities.Skills.Predefined;
using DnD.GameManagers.Dice;
using System.Collections.Generic;

internal class Wizard : IDndClass
{
    public EDiceType HitDie => EDiceType.D6;

    public EAttributeType SavingThrowProficiencies => EAttributeType.Intelligence | EAttributeType.Wisdom;

    public EArmorType ArmorProficiencies => EArmorType.None;

    public EWeaponType WeaponProficiencies => EWeaponType.Dagger | EWeaponType.Dart | EWeaponType.Sling | EWeaponType.Quarterstaff | EWeaponType.CrossbowLight;

    public string Name => "Wizard";

    public string Description => "A scholarly magic-user capable of manipulating the structures of reality";

    public List<IDndSkill> ChoosableSkillProficiencies => new List<IDndSkill>()
    {
        Arcana.Instance,
        History.Instance,
        Insight.Instance,
        Investigation.Instance,
        Medicine.Instance,
        Religion.Instance
    };

    public int NumberOfSkillProficiencies => 2;

    private Wizard() { }

    public static readonly Wizard Instance = new Wizard();
}
