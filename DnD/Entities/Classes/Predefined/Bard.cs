namespace DnD.Entities.Classes.Predefined;

using DnD.Entities.Attributes;
using DnD.Entities.Items.Equipments.Armors;
using DnD.Entities.Items.Equipments.Weapons;
using DnD.Entities.Skills;
using DnD.Entities.Skills.Predefined;
using DnD.GameManagers.Dice;
using System.Collections.Generic;

internal class Bard : IDndClass
{
    public EDiceType HitDie => EDiceType.D8;

    public EAttributeType SavingThrowProficiencies => EAttributeType.Dexterity | EAttributeType.Charisma;

    public EArmorType ArmorProficiencies => EArmorType.Light;

    public EWeaponType WeaponProficiencies => EWeaponType.SimpleWeapon | EWeaponType.CrossbowHand | EWeaponType.Longsword | EWeaponType.Rapier | EWeaponType.Shortsword;

    public string Name => "Bard";

    public string Description => "An inspiring magician whose power echoes the music of creation";

    public List<IDndSkill> ChoosableSkillProficiencies => new List<IDndSkill>
    {
        Acrobatics.Instance,
        AnimalHandling.Instance,
        Arcana.Instance,
        Athletics.Instance,
        Deception.Instance,
        History.Instance,
        Insight.Instance,
        Intimidation.Instance,
        Investigation.Instance,
        Medicine.Instance,
        Nature.Instance,
        Perception.Instance,
        Performance.Instance,
        Persuasion.Instance,
        Religion.Instance,
        SleightOfHand.Instance,
        Stealth.Instance,
        Survival.Instance
    };

    public int NumberOfSkillProficiencies => 3;

    private Bard() { }

    public static readonly Bard Instance = new Bard();
}
