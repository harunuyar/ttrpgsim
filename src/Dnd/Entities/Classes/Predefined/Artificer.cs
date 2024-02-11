namespace Dnd.Entities.Classes.Predefined;

using Dnd.Entities.Attributes;
using Dnd.Entities.Items.Equipments.Armors;
using Dnd.Entities.Items.Equipments.Weapons;
using Dnd.Entities.Skills;
using Dnd.Entities.Skills.Predefined;
using Dnd.GameManagers.Dice;
using System.Collections.Generic;

public class Artificer : IDndClass
{
    public EDiceType HitDie => EDiceType.d8;

    public EAttributeType SavingThrowProficiencies => EAttributeType.Constitution | EAttributeType.Intelligence;

    public EArmorType ArmorProficiencies => EArmorType.Light | EArmorType.Medium | EArmorType.Shield;

    public EWeaponType WeaponProficiencies => EWeaponType.SimpleWeapon;

    public string Name => "Artificer";

    public string Description => "A master of unlocking magic in everyday objects, artificers are supreme inventors. They see magic as a complex system waiting to be decoded and then harnessed in their spells and inventions.";

    public List<IDndSkill> ChoosableSkillProficiencies => new()
    {
        Arcana.Instance,
        History.Instance,
        Investigation.Instance,
        Medicine.Instance,
        Nature.Instance,
        Perception.Instance,
        SleightOfHand.Instance
    };

    public int NumberOfSkillProficiencies => 2;

    private Artificer() { }

    public static readonly Artificer Instance = new Artificer();
}
