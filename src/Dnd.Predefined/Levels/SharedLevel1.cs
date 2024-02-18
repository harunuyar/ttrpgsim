namespace Dnd.Predefined.Levels;

using Dnd.Predefined.Feats;
using Dnd.Predefined.Feats.Proficiency;
using Dnd.System.Entities.Classes;
using Dnd.System.Entities.Feats;
using Dnd.System.Entities.Items.Tools;
using Dnd.System.Entities.Levels;
using Dnd.System.Entities.Skills;

public abstract class SharedLevel1 : ILevel
{
    public SharedLevel1(IClass dndClass, string name, bool multiclass, List<ISkill> proficientSkills, EToolType toolProficiencies = EToolType.None)
    {
        Class = dndClass;
        Name = name;

        if (multiclass)
        {
            Feats = new List<IFeat>(3 + proficientSkills.Count)
            {
                new ArmorProficiency(dndClass.MulticlassArmorProficiencies),
                new WeaponProficiency(dndClass.MulticlassWeaponProficiencies),
                new ToolProficiency(dndClass.MulticlassToolProficiencies),
            };
        }
        else
        {
            Feats = new List<IFeat>(6 + proficientSkills.Count)
            {
                new UnarmedStrikeAbility(),
                new ProficiencyBonus(),
                new ArmorProficiency(dndClass.ArmorProficiencies),
                new WeaponProficiency(dndClass.WeaponProficiencies),
                new SavingThrowProficiency(dndClass.SavingThrowProficiencies),
                new ToolProficiency(dndClass.ToolProficiencies | toolProficiencies),
            };
        }

        foreach (var skill in proficientSkills)
        {
            Feats.Add(new SkillProficiency(skill, EProficiencyType.FullProficiency));
        }
    }

    public int Level => 1;

    public IClass Class { get; }

    public List<IFeat> Feats { get; }

    public string Name { get; }

    public string? Subclass => null;
}
