namespace Dnd.Predefined.Classes;

using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.Items.Equipments.Armors;
using Dnd.System.Entities.Items.Equipments.Weapons;
using Dnd.System.Entities.Skills;
using Dnd.GameManagers.Dice;
using Dnd.System.Entities.Classes;
using Dnd.Predefined.Skills;

public class Warlock : IClass
{
    public EDiceType HitDie => EDiceType.d8;

    public EAttributeType SavingThrowProficiencies => EAttributeType.Wisdom | EAttributeType.Charisma;

    public EArmorType ArmorProficiencies => EArmorType.Light;

    public EWeaponType WeaponProficiencies => EWeaponType.SimpleWeapon;

    public string Name => "Warlock";

    public string Description => "A wielder of magic that is derived from a bargain with an extraplanar entity";

    public List<ISkill> ChoosableSkillProficiencies => new List<ISkill>()
    {
        Arcana.Instance, 
        Deception.Instance, 
        History.Instance, 
        Intimidation.Instance, 
        Investigation.Instance, 
        Nature.Instance, 
        Religion.Instance
    };

    public int NumberOfSkillProficiencies => 2;

    private Warlock() { }

    public static readonly Warlock Instance = new Warlock();
}
