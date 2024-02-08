namespace DnD.Entities.Classes.Predefined;

using DnD.Entities.Attributes;
using DnD.Entities.Items.Equipments.Armors;
using DnD.Entities.Items.Equipments.Weapons;
using DnD.Entities.Skills;
using DnD.Entities.Skills.Predefined;
using DnD.GameManagers.Dice;
using System.Collections.Generic;

internal class Monk : IDndClass
{
    public EDiceType HitDie => EDiceType.D8;

    public EAttributeType SavingThrowProficiencies => EAttributeType.Strength | EAttributeType.Dexterity;

    public EArmorType ArmorProficiencies => EArmorType.None;

    public EWeaponType WeaponProficiencies => EWeaponType.SimpleWeapon | EWeaponType.Shortsword;

    public string Name => "Monk";

    public string Description => "A master of martial arts, harnessing the power of the body in pursuit of physical and spiritual perfection";

    public List<IDndSkill> ChoosableSkillProficiencies => new List<IDndSkill>()
    {
        Acrobatics.Instance, 
        Athletics.Instance, 
        History.Instance,
        Insight.Instance, 
        Religion.Instance, 
        Stealth.Instance
    };

    public int NumberOfSkillProficiencies => 2;

    private Monk() { }

    public static readonly Monk Instance = new Monk();
}
