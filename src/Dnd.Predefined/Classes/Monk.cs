﻿namespace Dnd.Predefined.Classes;

using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.Items.Equipments.Armors;
using Dnd.System.Entities.Items.Equipments.Weapons;
using Dnd.System.Entities.Skills;
using Dnd.GameManagers.Dice;
using Dnd.System.Entities.Classes;
using Dnd.Predefined.Skills;

public class Monk : IClass
{
    public EDiceType HitDie => EDiceType.d8;

    public EAttributeType SavingThrowProficiencies => EAttributeType.Strength | EAttributeType.Dexterity;

    public EArmorType ArmorProficiencies => EArmorType.None;

    public EWeaponType WeaponProficiencies => EWeaponType.SimpleWeapon | EWeaponType.Shortsword;

    public string Name => "Monk";

    public string Description => "A master of martial arts, harnessing the power of the body in pursuit of physical and spiritual perfection";

    public List<ISkill> ChoosableSkillProficiencies => new List<ISkill>()
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