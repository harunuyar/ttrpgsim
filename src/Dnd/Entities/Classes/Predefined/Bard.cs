namespace Dnd.Entities.Classes.Predefined;

using Dnd.Entities.Attributes;
using Dnd.Entities.Items.Equipments.Armors;
using Dnd.Entities.Items.Equipments.Weapons;
using Dnd.Entities.Skills;
using Dnd.Entities.Skills.Predefined;
using Dnd.GameManagers.Dice;
using System.Collections.Generic;

public class Bard : IDndClass
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
