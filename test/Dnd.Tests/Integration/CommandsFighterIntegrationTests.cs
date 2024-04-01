namespace Dnd.Tests.Integration;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.AbilityScore;
using Dnd._5eSRD.Models.Alignment;
using Dnd._5eSRD.Models.Common;
using Dnd._5eSRD.Models.DamageType;
using Dnd._5eSRD.Models.Language;
using Dnd._5eSRD.Models.Proficiency;
using Dnd._5eSRD.Models.Race;
using Dnd._5eSRD.Models.Skill;
using Dnd.Context;
using Dnd.Predefined.Commands.BoolCommands;
using Dnd.Predefined.Commands.ListCommands;
using Dnd.Predefined.Commands.ScoreCommands;
using Dnd.Predefined.Commands.ValueCommands;
using Dnd.Predefined.Commands.VoidCommands;
using Dnd.Predefined.Definitions;
using Dnd.Predefined.Definitions.Actions.Fighter;
using Dnd.Predefined.Definitions.Classes;
using Dnd.Predefined.Definitions.Features.Fighter;
using Dnd.Predefined.GameActor;
using Dnd.Predefined.Instances;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.GameActor;
using Dnd.System.Entities.Instances;

[TestClass]
public class CommandsFighterIntegrationTests
{
    private IGameActor? player;

    [TestInitialize]
    public async Task Setup()
    {
        var humanModel = await DndContext.Instance.GetObject<RaceModel>(Races.Human);
        Assert.IsNotNull(humanModel);

        var elvish = await DndContext.Instance.GetObject<LanguageModel>(Languages.Elvish);
        Assert.IsNotNull(elvish);

        IRaceInstance race = await RaceInstance.Create(humanModel, abilityBonusOptions: [], languageOptions: [elvish], startingProficiencyOptions: [], traits: []);
        Assert.IsNotNull(race);

        var chaoticGoodModel = await DndContext.Instance.GetObject<AlignmentModel>(Alignments.ChaoticGood);
        Assert.IsNotNull(chaoticGoodModel);

        var chaoticGood = new AlignmentInstance(chaoticGoodModel);

        var athleticsProficiency = await DndContext.Instance.GetObject<ProficiencyModel>(Proficiencies.SkillAthletics);
        Assert.IsNotNull(athleticsProficiency);

        var intimidationProficiency = await DndContext.Instance.GetObject<ProficiencyModel>(Proficiencies.SkillIntimidation);
        Assert.IsNotNull(intimidationProficiency);

        var fighterClass = await Fighter.Create([athleticsProficiency, intimidationProficiency]);
        Assert.IsNotNull(fighterClass);

        var defense = await FightingStyleDefense.Create();
        Assert.IsNotNull(defense);

        var fighterLevel1 = await LevelFactory.FighterLevel1(fighterClass, defense);
        Assert.IsNotNull(fighterLevel1);

        player = new GameActor("Jack", race, null, chaoticGood, fighterLevel1);

        var str = await DndContext.Instance.GetObject<AbilityScoreModel>(AbilityScores.Str);
        Assert.IsNotNull(str);
        var dex = await DndContext.Instance.GetObject<AbilityScoreModel>(AbilityScores.Dex);
        Assert.IsNotNull(dex);
        var con = await DndContext.Instance.GetObject<AbilityScoreModel>(AbilityScores.Con);
        Assert.IsNotNull(con);
        var inte = await DndContext.Instance.GetObject<AbilityScoreModel>(AbilityScores.Int);
        Assert.IsNotNull(inte);
        var wis = await DndContext.Instance.GetObject<AbilityScoreModel>(AbilityScores.Wis);
        Assert.IsNotNull(wis);
        var cha = await DndContext.Instance.GetObject<AbilityScoreModel>(AbilityScores.Cha);
        Assert.IsNotNull(cha);

        player.AttributeSet.AddAbilityScore(str, 17);
        player.AttributeSet.AddAbilityScore(dex, 14);
        player.AttributeSet.AddAbilityScore(con, 16);
        player.AttributeSet.AddAbilityScore(inte, 10);
        player.AttributeSet.AddAbilityScore(wis, 12);
        player.AttributeSet.AddAbilityScore(cha, 8);
    }

    [TestMethod]
    public async Task TestHitPoint()
    {
        Assert.IsNotNull(player);

        var maxHp = await new GetMaxHP(player).Execute();
        Assert.IsTrue(maxHp.IsSuccess);

        Assert.AreEqual(13, maxHp.Value); // 10 + 3 (Con)

        var fighterLevel1 = player.LevelInfo.GetLastLevels().FirstOrDefault();
        Assert.IsNotNull(fighterLevel1);
        Assert.AreEqual(1, fighterLevel1.LevelModel.LevelNumber);

        var fighterLevel2 = await LevelFactory.FighterLevel2(fighterLevel1);
        Assert.IsNotNull(fighterLevel2);

        var addLevel = await new AddLevel(player, fighterLevel2, 6).Execute();
        Assert.IsTrue(addLevel.IsSuccess);

        maxHp = await new GetMaxHP(player).Execute();
        Assert.IsTrue(maxHp.IsSuccess);

        Assert.AreEqual(22, maxHp.Value); // 10 (Level 1) + 6 (Level 2) + 2 * 3 (Con)
    }

    [TestMethod]
    public async Task TestArmorClass()
    {
        Assert.IsNotNull(player);

        var ac = await new GetArmorClass(player).Execute();
        Assert.IsTrue(ac.IsSuccess);

        Assert.AreEqual(12, ac.Value); // 10 + 2 (Dex)
    }

    [TestMethod]
    public async Task TestApplyDamage()
    {
        Assert.IsNotNull(player);

        Assert.AreEqual(10, player.HitPoints.CurrentHitPoints);

        var slashingDamage = await DndContext.Instance.GetObject<DamageTypeModel>(DamageTypes.Slashing);
        Assert.IsNotNull(slashingDamage);

        var damage = await new ApplyDamage(player, 5, slashingDamage).Execute();
        Assert.IsTrue(damage.IsSuccess);

        Assert.AreEqual(5, player.HitPoints.CurrentHitPoints);

        damage = await new ApplyDamage(player, 3, slashingDamage).Execute();
        Assert.IsTrue(damage.IsSuccess);

        Assert.AreEqual(2, player.HitPoints.CurrentHitPoints);

        damage = await new ApplyDamage(player, 2, slashingDamage).Execute();
        Assert.IsTrue(damage.IsSuccess);

        Assert.AreEqual(0, player.HitPoints.CurrentHitPoints);
    }

    [TestMethod]
    public async Task TestApplyHealing()
    {
        Assert.IsNotNull(player);

        player.HitPoints.Damage(9);
        Assert.AreEqual(1, player.HitPoints.CurrentHitPoints);

        var healing = await new ApplyHeal(player, 4).Execute();
        Assert.IsTrue(healing.IsSuccess);

        Assert.AreEqual(5, player.HitPoints.CurrentHitPoints);

        healing = await new ApplyHeal(player, 3).Execute();
        Assert.IsTrue(healing.IsSuccess);

        Assert.AreEqual(8, player.HitPoints.CurrentHitPoints);

        healing = await new ApplyHeal(player, 2).Execute();
        Assert.IsTrue(healing.IsSuccess);

        Assert.AreEqual(10, player.HitPoints.CurrentHitPoints);
    }

    [TestMethod]
    public async Task TestGetAlignment()
    {
        Assert.IsNotNull(player);

        var alignment = await new GetAlignment(player).Execute();
        Assert.IsTrue(alignment.IsSuccess);

        Assert.AreEqual(Alignments.ChaoticGood, alignment.Value?.Url);
    }

    [TestMethod]
    public async Task TestGetCreatureSize()
    {
        Assert.IsNotNull(player);

        var size = await new GetCreatureSize(player).Execute();
        Assert.IsTrue(size.IsSuccess);

        Assert.AreEqual(ECreatureSize.Medium, size.Value);
    }

    [TestMethod]
    public async Task TestGetCreatureType()
    {
        Assert.IsNotNull(player);

        var type = await new GetCreatureType(player).Execute();
        Assert.IsTrue(type.IsSuccess);

        Assert.AreEqual(ECreatureType.Humanoid, type.Value);
    }

    [TestMethod]
    public async Task TestGetSpeed()
    {
        Assert.IsNotNull(player);

        var speed = await new GetSpeed(player).Execute();
        Assert.IsTrue(speed.IsSuccess);

        Assert.AreEqual(30, speed.Value);
    }

    [TestMethod]
    public async Task TestCalculateDamage()
    {
        Assert.IsNotNull(player);

        var slashingDamage = await DndContext.Instance.GetObject<DamageTypeModel>(DamageTypes.Slashing);
        Assert.IsNotNull(slashingDamage);

        var damage = await new CalculateDamage(player, 5, slashingDamage).Execute();
        Assert.IsTrue(damage.IsSuccess);

        Assert.AreEqual(5, damage.Value);
    }

    [TestMethod]
    public async Task TestCalculateHeal()
    {
        Assert.IsNotNull(player);

        var heal = await new CalculateHeal(player, 5).Execute();
        Assert.IsTrue(heal.IsSuccess);

        Assert.AreEqual(5, heal.Value);
    }

    [TestMethod]
    public async Task TestGetAbilityModifier()
    {
        Assert.IsNotNull(player);

        var str = await DndContext.Instance.GetObject<AbilityScoreModel>(AbilityScores.Str);
        Assert.IsNotNull(str);

        var modifier = await new GetAbilityModifier(player, str).Execute();
        Assert.IsTrue(modifier.IsSuccess);

        Assert.AreEqual(4, modifier.Value); // (17 + 1) / 2 - 5

        var cha = await DndContext.Instance.GetObject<AbilityScoreModel>(AbilityScores.Cha);
        Assert.IsNotNull(cha);

        modifier = await new GetAbilityModifier(player, cha).Execute();
        Assert.IsTrue(modifier.IsSuccess);

        Assert.AreEqual(-1, modifier.Value); // (8 + 1) / 2 - 5

        var wis = await DndContext.Instance.GetObject<AbilityScoreModel>(AbilityScores.Wis);
        Assert.IsNotNull(wis);

        modifier = await new GetAbilityModifier(player, wis).Execute();
        Assert.IsTrue(modifier.IsSuccess);

        Assert.AreEqual(1, modifier.Value); // (12 + 1) / 2 - 5

        var dex = await DndContext.Instance.GetObject<AbilityScoreModel>(AbilityScores.Dex);
        Assert.IsNotNull(dex);

        modifier = await new GetAbilityModifier(player, dex).Execute();
        Assert.IsTrue(modifier.IsSuccess);

        Assert.AreEqual(2, modifier.Value); // (14 + 1) / 2 - 5

        var con = await DndContext.Instance.GetObject<AbilityScoreModel>(AbilityScores.Con);
        Assert.IsNotNull(con);

        modifier = await new GetAbilityModifier(player, con).Execute();
        Assert.IsTrue(modifier.IsSuccess);

        Assert.AreEqual(3, modifier.Value); // (16 + 1) / 2 - 5

        var inte = await DndContext.Instance.GetObject<AbilityScoreModel>(AbilityScores.Int);
        Assert.IsNotNull(inte);

        modifier = await new GetAbilityModifier(player, inte).Execute();
        Assert.IsTrue(modifier.IsSuccess);

        Assert.AreEqual(0, modifier.Value); // (10 + 1) / 2 - 5
    }

    [TestMethod]
    public async Task TestBaseAbilityScore()
    {
        Assert.IsNotNull(player);

        var str = await DndContext.Instance.GetObject<AbilityScoreModel>(AbilityScores.Str);
        Assert.IsNotNull(str);

        var score = await new GetBaseAbilityScore(player, str).Execute();
        Assert.IsTrue(score.IsSuccess);

        Assert.AreEqual(18, score.Value);

        var cha = await DndContext.Instance.GetObject<AbilityScoreModel>(AbilityScores.Cha);
        Assert.IsNotNull(cha);

        score = await new GetBaseAbilityScore(player, cha).Execute();
        Assert.IsTrue(score.IsSuccess);

        Assert.AreEqual(9, score.Value);

        var wis = await DndContext.Instance.GetObject<AbilityScoreModel>(AbilityScores.Wis);
        Assert.IsNotNull(wis);

        score = await new GetBaseAbilityScore(player, wis).Execute();
        Assert.IsTrue(score.IsSuccess);

        Assert.AreEqual(13, score.Value);

        var dex = await DndContext.Instance.GetObject<AbilityScoreModel>(AbilityScores.Dex);
        Assert.IsNotNull(dex);

        score = await new GetBaseAbilityScore(player, dex).Execute();
        Assert.IsTrue(score.IsSuccess);

        Assert.AreEqual(15, score.Value);

        var con = await DndContext.Instance.GetObject<AbilityScoreModel>(AbilityScores.Con);
        Assert.IsNotNull(con);

        score = await new GetBaseAbilityScore(player, con).Execute();
        Assert.IsTrue(score.IsSuccess);

        Assert.AreEqual(17, score.Value);

        var inte = await DndContext.Instance.GetObject<AbilityScoreModel>(AbilityScores.Int);
        Assert.IsNotNull(inte);

        score = await new GetBaseAbilityScore(player, inte).Execute();
        Assert.IsTrue(score.IsSuccess);

        Assert.AreEqual(11, score.Value);
    }

    [TestMethod]
    public async Task TestTotalAbilityScore()
    {
        Assert.IsNotNull(player);

        var str = await DndContext.Instance.GetObject<AbilityScoreModel>(AbilityScores.Str);
        Assert.IsNotNull(str);

        var score = await new GetTotalAbilityScore(player, str).Execute();
        Assert.IsTrue(score.IsSuccess);

        Assert.AreEqual(18, score.Value);

        var cha = await DndContext.Instance.GetObject<AbilityScoreModel>(AbilityScores.Cha);
        Assert.IsNotNull(cha);

        score = await new GetTotalAbilityScore(player, cha).Execute();
        Assert.IsTrue(score.IsSuccess);

        Assert.AreEqual(9, score.Value);

        var wis = await DndContext.Instance.GetObject<AbilityScoreModel>(AbilityScores.Wis);
        Assert.IsNotNull(wis);

        score = await new GetTotalAbilityScore(player, wis).Execute();
        Assert.IsTrue(score.IsSuccess);

        Assert.AreEqual(13, score.Value);

        var dex = await DndContext.Instance.GetObject<AbilityScoreModel>(AbilityScores.Dex);
        Assert.IsNotNull(dex);

        score = await new GetTotalAbilityScore(player, dex).Execute();
        Assert.IsTrue(score.IsSuccess);

        Assert.AreEqual(15, score.Value);

        var con = await DndContext.Instance.GetObject<AbilityScoreModel>(AbilityScores.Con);
        Assert.IsNotNull(con);

        score = await new GetTotalAbilityScore(player, con).Execute();
        Assert.IsTrue(score.IsSuccess);

        Assert.AreEqual(17, score.Value);

        var inte = await DndContext.Instance.GetObject<AbilityScoreModel>(AbilityScores.Int);
        Assert.IsNotNull(inte);

        score = await new GetTotalAbilityScore(player, inte).Execute();
        Assert.IsTrue(score.IsSuccess);

        Assert.AreEqual(11, score.Value);
    }

    [TestMethod]
    public async Task TestGetConcentrationSavingDC()
    {
        Assert.IsNotNull(player);

        var slashingDamage = await DndContext.Instance.GetObject<DamageTypeModel>(DamageTypes.Slashing);
        Assert.IsNotNull(slashingDamage);

        var dc = await new GetConcentrationSavingDC(player, 7, null).Execute();
        Assert.IsTrue(dc.IsSuccess);

        Assert.AreEqual(10, dc.Value);

        dc = await new GetConcentrationSavingDC(player, 30, null).Execute();
        Assert.IsTrue(dc.IsSuccess);

        Assert.AreEqual(15, dc.Value);

        dc = await new GetConcentrationSavingDC(player, 27, null).Execute();
        Assert.IsTrue(dc.IsSuccess);

        Assert.AreEqual(13, dc.Value);
    }

    [TestMethod]
    public async Task TestCriticalFailureThreshold()
    {
        Assert.IsNotNull(player);

        var threshold = await new GetCriticalFailureThreshold(player, null).Execute();
        Assert.IsTrue(threshold.IsSuccess);

        Assert.AreEqual(1, threshold.Value);
    }

    [TestMethod]
    public async Task TestCriticalSuccessThreshold()
    {
        Assert.IsNotNull(player);

        var threshold = await new GetCriticalSuccessThreshold(player, null).Execute();
        Assert.IsTrue(threshold.IsSuccess);

        Assert.AreEqual(20, threshold.Value);
    }

    [TestMethod]
    public async Task TestGetMaxSpellSlotsCount()
    {
        Assert.IsNotNull(player);

        var count = await new GetMaxSpellSlotsCount(player, spellLevel: 1).Execute();
        Assert.IsTrue(count.IsSuccess);

        Assert.AreEqual(0, count.Value);
    }

    [TestMethod]
    public async Task TestGetPassiveSkillValue()
    {
        Assert.IsNotNull(player);

        var athletics = await DndContext.Instance.GetObject<SkillModel>(Skills.Athletics);
        Assert.IsNotNull(athletics);

        var value = await new GetPassiveSkillValue(player, athletics).Execute();
        Assert.IsTrue(value.IsSuccess);

        Assert.AreEqual(16, value.Value);

        var intimidation = await DndContext.Instance.GetObject<SkillModel>(Skills.Intimidation);
        Assert.IsNotNull(intimidation);

        value = await new GetPassiveSkillValue(player, intimidation).Execute();
        Assert.IsTrue(value.IsSuccess);

        Assert.AreEqual(11, value.Value);

        var insight = await DndContext.Instance.GetObject<SkillModel>(Skills.Insight);
        Assert.IsNotNull(insight);

        value = await new GetPassiveSkillValue(player, insight).Execute();
        Assert.IsTrue(value.IsSuccess);

        Assert.AreEqual(11, value.Value);
    }

    [TestMethod]
    public async Task TestGetProficiencyBonus()
    {
        Assert.IsNotNull(player);

        var bonus = await new GetProficiencyBonus(player).Execute();
        Assert.IsTrue(bonus.IsSuccess);

        Assert.AreEqual(2, bonus.Value);
    }

    [TestMethod]
    public async Task TestGetSpellcastingLevel()
    {
        Assert.IsNotNull(player);

        var level = await new GetSpellcastingLevel(player).Execute();
        Assert.IsTrue(level.IsSuccess);

        Assert.AreEqual(0, level.Value);
    }

    [TestMethod]
    public async Task TestGetActions()
    {
        Assert.IsNotNull(player);

        var actions = await new GetActions(player).Execute();
        Assert.IsTrue(actions.IsSuccess);

        Assert.AreEqual(3, actions.Values.Count); // Main hand unarmed attack, off hand unarmed attack, Second Wind

        foreach (var action in actions.Values.Select(x => x.Item2))
        {
            var available = await new IsActionAvailable(player, action, null).Execute();
            Assert.IsTrue(available.IsSuccess);

            Assert.IsTrue(available.Value);
        }

        var secondWind = actions.Values.Select(x => x.Item2).FirstOrDefault(x => x is SecondWindAction);
        Assert.IsNotNull(secondWind);

        Assert.AreEqual(1, secondWind.UsageLimits.Count);
        Assert.AreEqual(EActionUsageLimitType.PerShortRest, secondWind.UsageLimits[0].Type);
        Assert.AreEqual(1, secondWind.UsageLimits[0].Limit);
        Assert.AreEqual(0, secondWind.UsageLimits[0].Current);

        secondWind.MarkUse();
        Assert.AreEqual(1, secondWind.UsageLimits[0].Current);
        Assert.AreEqual(true, secondWind.UsageLimits[0].IsExhausted);

        var isAvailable = await new IsActionAvailable(player, secondWind, null).Execute();

        Assert.IsFalse(isAvailable.Value);
    }

    [TestMethod]
    public async Task TestGetSpokenLanguages()
    {
        Assert.IsNotNull(player);

        var langs = await new GetSpokenLanguages(player).Execute();
        Assert.IsTrue(langs.IsSuccess);

        Assert.AreEqual(2, langs.Values.Count);

        var urls = langs.Values.Select(x => x.Item2.Url);
        Assert.IsTrue(urls.Contains(Languages.Common));
        Assert.IsTrue(urls.Contains(Languages.Elvish));
    }
}
