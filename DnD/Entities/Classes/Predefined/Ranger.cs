namespace DnD.Entities.Classes.Predefined;

using DnD.Entities.Attributes;
using DnD.Entities.Items.Equipments.Armors;
using DnD.Entities.Items.Equipments.Weapons;
using DnD.Entities.Skills;
using DnD.Entities.Skills.Predefined;
using DnD.GameManagers.Dice;
using System.Collections.Generic;

internal class Ranger : IDndClass
{
    public EDiceType HitDie => EDiceType.D10;

    public EAttributeType SavingThrowProficiencies => EAttributeType.Strength | EAttributeType.Dexterity;

    public EArmorType ArmorProficiencies => EArmorType.Light | EArmorType.Medium | EArmorType.Shield;

    public EWeaponType WeaponProficiencies => EWeaponType.All;

    public string Name => "Ranger";

    public string Description => "A warrior who combats threats on the edges of civilization";

    public List<IDndSkill> ChoosableSkillProficiencies => new List<IDndSkill>()
    {
        AnimalHandling.Instance,
        Athletics.Instance,
        Insight.Instance,
        Investigation.Instance,
        Nature.Instance,
        Perception.Instance,
        Stealth.Instance,
        Survival.Instance
    };

    public int NumberOfSkillProficiencies => 3;

    private Ranger() { }

    public static readonly Ranger Instance = new Ranger();
}
