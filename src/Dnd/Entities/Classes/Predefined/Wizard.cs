namespace Dnd.Entities.Classes.Predefined;

using Dnd.Entities.Attributes;
using Dnd.Entities.Items.Equipments.Armors;
using Dnd.Entities.Items.Equipments.Weapons;
using Dnd.Entities.Skills;
using Dnd.Entities.Skills.Predefined;
using Dnd.GameManagers.Dice;
using System.Collections.Generic;

public class Wizard : IDndClass
{
    public EDiceType HitDie => EDiceType.d6;

    public EAttributeType SavingThrowProficiencies => EAttributeType.Intelligence | EAttributeType.Wisdom;

    public EArmorType ArmorProficiencies => EArmorType.None;

    public EWeaponType WeaponProficiencies => EWeaponType.Dagger | EWeaponType.Dart | EWeaponType.Sling | EWeaponType.Quarterstaff | EWeaponType.CrossbowLight;

    public string Name => "Wizard";

    public string Description => "A scholarly magic-user capable of manipulating the structures of reality";

    public List<IDndSkill> ChoosableSkillProficiencies => new List<IDndSkill>()
    {
        Arcana.Instance,
        History.Instance,
        Insight.Instance,
        Investigation.Instance,
        Medicine.Instance,
        Religion.Instance
    };

    public int NumberOfSkillProficiencies => 2;

    private Wizard() { }

    public static readonly Wizard Instance = new Wizard();
}
