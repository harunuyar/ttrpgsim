namespace Dnd.Predefined.Classes;

using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.Items.Equipments.Armors;
using Dnd.System.Entities.Items.Equipments.Weapons;
using Dnd.System.Entities.Skills;
using Dnd.GameManagers.Dice;
using Dnd.System.Entities.Classes;
using Dnd.Predefined.Skills;

public class Barbarian : IClass
{
    public EDiceType HitDie => EDiceType.d12;

    public EAttributeType SavingThrowProficiencies => EAttributeType.Strength | EAttributeType.Constitution;

    public EArmorType ArmorProficiencies => EArmorType.Light | EArmorType.Medium | EArmorType.Shield;

    public EWeaponType WeaponProficiencies => EWeaponType.SimpleWeapon | EWeaponType.MartialWeapon;

    public string Name => "Barbarian";

    public string Description => "A fierce warrior of primitive background who can enter a battle rage";

    public List<ISkill> ChoosableSkillProficiencies => new List<ISkill>
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
