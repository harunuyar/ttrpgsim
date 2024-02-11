namespace Dnd.Entities.Classes.Predefined;

using Dnd.Entities.Attributes;
using Dnd.Entities.Items.Equipments.Armors;
using Dnd.Entities.Items.Equipments.Weapons;
using Dnd.Entities.Skills;
using Dnd.Entities.Skills.Predefined;
using Dnd.GameManagers.Dice;
using System.Collections.Generic;

public class Warlock : IDndClass
{
    public EDiceType HitDie => EDiceType.d8;

    public EAttributeType SavingThrowProficiencies => EAttributeType.Wisdom | EAttributeType.Charisma;

    public EArmorType ArmorProficiencies => EArmorType.Light;

    public EWeaponType WeaponProficiencies => EWeaponType.SimpleWeapon;

    public string Name => "Warlock";

    public string Description => "A wielder of magic that is derived from a bargain with an extraplanar entity";

    public List<IDndSkill> ChoosableSkillProficiencies => new List<IDndSkill>()
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
