namespace Dnd.Entities.Classes.Predefined;

using Dnd.Entities.Attributes;
using Dnd.Entities.Items.Equipments.Armors;
using Dnd.Entities.Items.Equipments.Weapons;
using Dnd.Entities.Skills;
using Dnd.Entities.Skills.Predefined;
using Dnd.GameManagers.Dice;
using System.Collections.Generic;

public class Cleric : IDndClass
{
    public EDiceType HitDie => EDiceType.d8;

    public EAttributeType SavingThrowProficiencies => EAttributeType.Wisdom | EAttributeType.Charisma;

    public EArmorType ArmorProficiencies => EArmorType.Light | EArmorType.Medium | EArmorType.Shield;

    public EWeaponType WeaponProficiencies => EWeaponType.SimpleWeapon;

    public string Name => "Cleric";

    public string Description => "A priestly champion who wields divine magic in service of a higher power.";

    public List<IDndSkill> ChoosableSkillProficiencies => new List<IDndSkill> 
    { 
        History.Instance, 
        Insight.Instance, 
        Medicine.Instance, 
        Persuasion.Instance,
        Religion.Instance
    };

    public int NumberOfSkillProficiencies => 2;

    private Cleric() { }

    public static readonly Cleric Instance = new Cleric();
}
