namespace DnD.Entities.Classes.Predefined;

using DnD.Entities.Attributes;
using DnD.Entities.Items.Equipments.Armors;
using DnD.Entities.Items.Equipments.Weapons;
using DnD.Entities.Skills;
using DnD.Entities.Skills.Predefined;
using DnD.GameManagers.Dice;
using System.Collections.Generic;

internal class Warlock : IDndClass
{
    public EDiceType HitDie => EDiceType.D8;

    public EAttributeType SavingThrowProficiencies => EAttributeType.Wisdom | EAttributeType.Charisma;

    public EArmorType ArmorProficiencies => EArmorType.Light;

    public EWeaponType WeaponProficiencies => EWeaponType.SimpleWeapon;

    public string Name => "Warlock";

    public string Description => "A wielder of magic that is derived from a bargain with an extraplanar entity";

    public List<IDndSkill> ChoosableSkillProficiencies => new List<IDndSkill>()
    {
        Arcana.Instance, 
        Deception.Instance, 
        History.Instance, 
        Intimidation.Instance, 
        Investigation.Instance, 
        Nature.Instance, 
        Religion.Instance
    };

    public int NumberOfSkillProficiencies => 2;

    private Warlock() { }

    public static readonly Warlock Instance = new Warlock();
}
