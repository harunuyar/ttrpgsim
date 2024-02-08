namespace DnD.Entities.Classes.Predefined;

using DnD.Entities.Attributes;
using DnD.Entities.Items.Equipments.Armors;
using DnD.Entities.Items.Equipments.Weapons;
using DnD.Entities.Skills;
using DnD.Entities.Skills.Predefined;
using DnD.GameManagers.Dice;
using System.Collections.Generic;

internal class Paladin : IDndClass
{
    public EDiceType HitDie => EDiceType.D10;

    public EAttributeType SavingThrowProficiencies => EAttributeType.Wisdom | EAttributeType.Charisma;

    public EArmorType ArmorProficiencies => EArmorType.All;

    public EWeaponType WeaponProficiencies => EWeaponType.All;

    public string Name => "Paladin";

    public string Description => "A holy warrior bound to a sacred oath";

    public List<IDndSkill> ChoosableSkillProficiencies => new List<IDndSkill>()
    {
        Athletics.Instance,
        Insight.Instance,
        Intimidation.Instance,
        Medicine.Instance,
        Persuasion.Instance,
        Religion.Instance
    };

    public int NumberOfSkillProficiencies => 2;

    private Paladin() { }

    public static readonly Paladin Instance = new Paladin();
}
