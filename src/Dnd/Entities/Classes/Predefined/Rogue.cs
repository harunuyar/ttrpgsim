namespace Dnd.Entities.Classes.Predefined;

using Dnd.Entities.Attributes;
using Dnd.Entities.Items.Equipments.Armors;
using Dnd.Entities.Items.Equipments.Weapons;
using Dnd.Entities.Skills;
using Dnd.Entities.Skills.Predefined;
using Dnd.GameManagers.Dice;
using System.Collections.Generic;

public class Rogue : IDndClass
{
    public EDiceType HitDie => EDiceType.D8;

    public EAttributeType SavingThrowProficiencies => EAttributeType.Dexterity | EAttributeType.Intelligence;

    public EArmorType ArmorProficiencies => EArmorType.Light;

    public EWeaponType WeaponProficiencies => EWeaponType.SimpleWeapon | EWeaponType.CrossbowHand | EWeaponType.Longsword | EWeaponType.Rapier | EWeaponType.Shortsword;

    public string Name => "Rogue";

    public string Description => "A scoundrel who uses stealth and trickery to overcome obstacles and enemies";

    public List<IDndSkill> ChoosableSkillProficiencies => new List<IDndSkill>()
    {
        Acrobatics.Instance,
        Athletics.Instance,
        Deception.Instance,
        Insight.Instance,
        Intimidation.Instance,
        Investigation.Instance,
        Perception.Instance,
        Performance.Instance,
        Persuasion.Instance,
        SleightOfHand.Instance,
        Stealth.Instance
    };

    public int NumberOfSkillProficiencies => 4;

    private Rogue() { }

    public static readonly Rogue Instance = new Rogue();
}
