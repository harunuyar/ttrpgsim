namespace Dnd.Entities.Classes.Predefined;

using Dnd.Entities.Attributes;
using Dnd.Entities.Items.Equipments.Armors;
using Dnd.Entities.Items.Equipments.Weapons;
using Dnd.Entities.Skills;
using Dnd.Entities.Skills.Predefined;
using Dnd.GameManagers.Dice;
using System.Collections.Generic;

public class Fighter : IDndClass
{
    public EDiceType HitDie => EDiceType.d10;

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
