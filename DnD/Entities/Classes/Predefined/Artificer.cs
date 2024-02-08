namespace DnD.Entities.Classes.Predefined;

using DnD.Entities.Attributes;
using DnD.Entities.Items.Equipments.Armors;
using DnD.Entities.Items.Equipments.Weapons;
using DnD.Entities.Skills;
using DnD.Entities.Skills.Predefined;
using DnD.GameManagers.Dice;
using System.Collections.Generic;

internal class Artificer : IDndClass
{
    public EDiceType HitDie => EDiceType.D8;

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
