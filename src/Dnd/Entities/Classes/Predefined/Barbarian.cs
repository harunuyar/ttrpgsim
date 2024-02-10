namespace Dnd.Entities.Classes.Predefined;

using Dnd.Entities.Attributes;
using Dnd.Entities.Items.Equipments.Armors;
using Dnd.Entities.Items.Equipments.Weapons;
using Dnd.Entities.Skills;
using Dnd.Entities.Skills.Predefined;
using Dnd.GameManagers.Dice;
using System.Collections.Generic;

public class Barbarian : IDndClass
{
    public EDiceType HitDie => EDiceType.D12;

    public EAttributeType SavingThrowProficiencies => EAttributeType.Strength | EAttributeType.Constitution;

    public EArmorType ArmorProficiencies => EArmorType.Light | EArmorType.Medium | EArmorType.Shield;

    public EWeaponType WeaponProficiencies => EWeaponType.SimpleWeapon | EWeaponType.MartialWeapon;

    public string Name => "Barbarian";

    public string Description => "A fierce warrior of primitive background who can enter a battle rage";

    public List<IDndSkill> ChoosableSkillProficiencies => new List<IDndSkill>
    {
        AnimalHandling.Instance, 
        Athletics.Instance, 
        Intimidation.Instance, 
        Nature.Instance, 
        Perception.Instance, 
        Survival.Instance 
    };

    public int NumberOfSkillProficiencies => 2;

    private Barbarian() { }

    public static readonly Barbarian Instance = new Barbarian();
}
