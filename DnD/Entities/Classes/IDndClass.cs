namespace DnD.Entities.Classes;

using DnD.Entities.Attributes;
using DnD.Entities.Items.Equipments.Armors;
using DnD.Entities.Items.Equipments.Weapons;
using DnD.Entities.Skills;
using DnD.GameManagers.Dice;
using TableTopRpg.Entities.Character;

internal interface IDndClass : IClass
{
    EDiceType HitDie { get; }
    EAttributeType SavingThrowProficiencies { get; }
    EArmorType ArmorProficiencies { get; }
    EWeaponType WeaponProficiencies { get; }
    List<IDndSkill> ChoosableSkillProficiencies { get; }
    int NumberOfSkillProficiencies { get; }
}
