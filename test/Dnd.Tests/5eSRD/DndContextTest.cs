namespace Dnd.Tests._5eSRD;

using Dnd.Context;
using Dnd._5eSRD.Models.AbilityScore;
using Dnd._5eSRD.Models.Alignment;
using Dnd._5eSRD.Models.Background;
using Dnd._5eSRD.Models.Class;
using Dnd._5eSRD.Models.Condition;
using Dnd._5eSRD.Models.DamageType;
using Dnd._5eSRD.Models.EquipmentCategory;
using Dnd._5eSRD.Models.Language;
using Dnd._5eSRD.Models.Level;
using Dnd._5eSRD.Models.MagicSchool;
using Dnd._5eSRD.Models.Monster;
using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.WeaponProperty;
using Dnd._5eSRD.Models.Trait;
using Dnd._5eSRD.Models.Subclass;
using Dnd._5eSRD.Models.Spell;
using Dnd._5eSRD.Models.Equipment;
using Dnd._5eSRD.Models.Feature;
using Dnd._5eSRD.Models.Feat;
using Dnd._5eSRD.Models.MagicItem;
using Dnd._5eSRD.Models.Proficiency;
using Dnd._5eSRD.Models.Race;
using Dnd._5eSRD.Models.Rule;
using Dnd._5eSRD.Models.RuleSection;
using Dnd._5eSRD.Models.Skill;

[TestClass]
public class DndContextTest
{
    [TestMethod]
    public void GetObject_AbilityScore_NotNull()
    {
        Assert.IsNotNull(DndContext.Instance.GetObject<AbilityScoreModel>(AbilityScores.Con).Result);
    }

    [TestMethod]
    public void GetObject_AbilityScore_Null()
    {
        Assert.IsNull(DndContext.Instance.GetObject<AbilityScoreModel>("").Result);
    }

    [TestMethod]
    public void GetObject_AbilityScore_NullWhenWrongType()
    {
        Assert.IsNull(DndContext.Instance.GetObject<AbilityScoreModel>(Alignments.ChaoticEvil).Result);
    }

    [TestMethod]
    public void GetObject_Alignment_NotNull()
    {
        Assert.IsNotNull(DndContext.Instance.GetObject<AlignmentModel>(Alignments.LawfulGood).Result);
    }

    [TestMethod]
    public void GetObject_Background_NotNull()
    {
        Assert.IsNotNull(DndContext.Instance.GetObject<BackgroundModel>(Backgrounds.Acolyte).Result);
    }

    [TestMethod]
    public void GetObject_Class_NotNull()
    {
        Assert.IsNotNull(DndContext.Instance.GetObject<ClassModel>(Classes.Barbarian).Result);
    }

    [TestMethod]
    public void GetObject_Condition_NotNull()
    {
        Assert.IsNotNull(DndContext.Instance.GetObject<ConditionModel>(Conditions.Blinded).Result);
    }

    [TestMethod]
    public void GetObject_DamageType_NotNull()
    {
        Assert.IsNotNull(DndContext.Instance.GetObject<DamageTypeModel>(DamageTypes.Acid).Result);
    }

    [TestMethod]
    public void GetObject_EquipmentCategory_NotNull()
    {
        Assert.IsNotNull(DndContext.Instance.GetObject<EquipmentCategoryModel>(EquipmentCategories.AdventuringGear).Result);
    }

    [TestMethod]
    public void GetObject_Equipment_NotNull()
    {
        Assert.IsNotNull(DndContext.Instance.GetObject<EquipmentModel>(Dnd._5eSRD.Constants.Equipment.Longsword).Result);
    }

    [TestMethod]
    public void GetObject_Feature_NotNull()
    {
        Assert.IsNotNull(DndContext.Instance.GetObject<FeatureModel >(Features.ArcaneRecovery).Result);
    }

    [TestMethod]
    public void GetObject_Feat_NotNull()
    {
        Assert.IsNotNull(DndContext.Instance.GetObject<FeatModel>(Feats.Grappler).Result);
    }

    [TestMethod]
    public void GetObject_Language_NotNull()
    {
        Assert.IsNotNull(DndContext.Instance.GetObject<LanguageModel>(Languages.Abyssal).Result);
    }

    [TestMethod]
    public void GetObject_Level_NotNull()
    {
        Assert.IsNotNull(DndContext.Instance.GetObject<LevelModel>(Levels.Bard3).Result);
    }

    [TestMethod]
    public void GetObject_MagicItem_NotNull()
    {
        Assert.IsNotNull(DndContext.Instance.GetObject<MagicItemModel>(MagicItems.AmuletOfHealth).Result);
    }

    [TestMethod]
    public void GetObject_MagicSchool_NotNull()
    {
        Assert.IsNotNull(DndContext.Instance.GetObject<MagicSchoolModel>(MagicSchools.Abjuration).Result);
    }

    [TestMethod]
    public void GetObject_MonsterType_NotNull()
    {
        Assert.IsNotNull(DndContext.Instance.GetObject<MonsterModel>(Monsters.Goblin).Result);
    }

    [TestMethod]
    public void GetObject_Proficiency_NotNull()
    {
        Assert.IsNotNull(DndContext.Instance.GetObject<ProficiencyModel>(Proficiencies.LightArmor).Result);
    }

    [TestMethod]
    public void GetObject_Race_NotNull()
    {
        Assert.IsNotNull(DndContext.Instance.GetObject<RaceModel>(Races.HalfElf).Result);
    }

    [TestMethod]
    public void GetObject_Rule_NotNull()
    {
        Assert.IsNotNull(DndContext.Instance.GetObject<RuleModel>(Rules.Equipment).Result);
    }

    [TestMethod]
    public void GetObject_RuleSection_NotNull()
    {
        Assert.IsNotNull(DndContext.Instance.GetObject<RuleSectionModel>(RuleSections.AbilityChecks).Result);
    }

    [TestMethod]
    public void GetObject_Skill_NotNull()
    {
        Assert.IsNotNull(DndContext.Instance.GetObject<SkillModel>(Skills.Acrobatics).Result);
    }

    [TestMethod]
    public void GetObject_Spell_NotNull()
    {
        Assert.IsNotNull(DndContext.Instance.GetObject<SpellModel>(Spells.AcidSplash).Result);
    }

    [TestMethod]
    public void GetObject_Subclass_NotNull()
    {
        Assert.IsNotNull(DndContext.Instance.GetObject<SubclassModel>(Subclasses.Champion).Result);
    }

    [TestMethod]
    public void GetObject_Trait_NotNull()
    {
        Assert.IsNotNull(DndContext.Instance.GetObject<TraitModel>(Traits.Brave).Result);
    }

    [TestMethod]
    public void GetObject_WeaponProperty_NotNull()
    {
        Assert.IsNotNull(DndContext.Instance.GetObject<WeaponPropertyModel>(WeaponProperties.Ammunition).Result);
    }
}
