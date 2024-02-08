namespace DnD.Entities.Classes.Predefined;

using DnD.Entities.Attributes;
using DnD.Entities.Items.Equipments.Armors;
using DnD.Entities.Items.Equipments.Weapons;
using DnD.Entities.Skills;
using DnD.Entities.Skills.Predefined;
using DnD.GameManagers.Dice;
using System.Collections.Generic;

internal class Fighter : IDndClass
{
    public EDiceType HitDie => EDiceType.D10;

    public EAttributeType SavingThrowProficiencies => EAttributeType.Strength | EAttributeType.Constitution;

    public EArmorType ArmorProficiencies => EArmorType.All;

    public EWeaponType WeaponProficiencies => EWeaponType.All;

    public string Name => "Fighter";

    public string Description => "A master of martial combat, skilled with a variety of weapons and armor";

    public List<IDndSkill> ChoosableSkillProficiencies => new List<IDndSkill>()
    {
        Acrobatics.Instance,
        AnimalHandling.Instance,
        Athletics.Instance,
        History.Instance,
        Insight.Instance,
        Intimidation.Instance,
        Perception.Instance,
        Survival.Instance
    };

    public int NumberOfSkillProficiencies => 2;

    private Fighter() { }

    public static readonly Fighter Instance = new Fighter();
}
