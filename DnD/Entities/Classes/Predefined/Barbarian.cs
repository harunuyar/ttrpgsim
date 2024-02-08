namespace DnD.Entities.Classes.Predefined;

using DnD.Entities.Attributes;
using DnD.Entities.Items.Equipments.Armors;
using DnD.Entities.Items.Equipments.Weapons;
using DnD.Entities.Skills;
using DnD.Entities.Skills.Predefined;
using DnD.GameManagers.Dice;
using System.Collections.Generic;

internal class Barbarian : IDndClass
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
