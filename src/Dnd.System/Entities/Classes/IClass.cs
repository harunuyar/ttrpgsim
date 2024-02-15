namespace Dnd.System.Entities.Classes;

using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.Items.Equipments.Armors;
using Dnd.System.Entities.Items.Equipments.Weapons;
using Dnd.System.Entities.Skills;
using Dnd.GameManagers.Dice;
using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Items.Tools;

public interface IClass : IDndEntity
{
    string Description { get; }

    EDiceType HitDie { get; }

    EAttributeType SpellCastingAttribute { get; }

    EAttributeType SavingThrowProficiencies { get; }

    EArmorType ArmorProficiencies { get; }

    EWeaponType WeaponProficiencies { get; }

    EToolType ToolProficiencies { get; }

    EToolType MulticlassToolProficiencies { get; }

    EArmorType MulticlassArmorProficiencies { get; }

    EWeaponType MulticlassWeaponProficiencies { get; }

    List<ISkill> ChoosableSkillProficiencies { get; }

    int NumberOfSkillProficiencies { get; }

    int MulticlassNumberOfSkillProficiencies { get; }

    bool MeetsPrerequisites(AttributeSet attributeSet);
}
