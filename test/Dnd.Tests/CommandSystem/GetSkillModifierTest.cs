namespace Dnd.Tests.CommandSystem;

using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
using Dnd.System.Entities.Advantage;
using Dnd.System.Entities.GameActors;
using Dnd.Predefined.Characters;
using Dnd.Predefined.Races;
using Dnd.Predefined.Skills;
using Dnd.Predefined.Armors.HeavyArmor;
using Dnd.System.Entities.Items;
using Dnd.Predefined.Levels.FighterLevels;
using Dnd.Predefined.Feats.Fighter.Level1.FightingStyle;

[TestClass]
public class GetSkillModifierTest
{
    private IGameActor? character;

    [TestInitialize]
    public void TestInitialize()
    {
        character = new CustomCharacter("Test", Dragonborn.Instance); // strength +2, charisma +1
        character.AttributeSet.Set(strength: 15, dexterity: 10, constitution: 14, intelligence: 8, wisdom: 12, charisma: 13);

        character.LevelInfo.AddLevel(new FighterLevel1(Athletics.Instance, Intimidation.Instance, Defense.Instance));

        character.Inventory.EquipArmor(new Item(ChainMailArmor.Instance));
    }

    [TestMethod]
    public void TestGetSkillModifierWithProficiency()
    {
        var getSkillModifierCommand = new GetSkillModifier(character!, Athletics.Instance);
        var result = getSkillModifierCommand.Execute();

        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(5, result.Value);
    }

    [TestMethod]
    public void TestGetSkillModifierWithoutProficiency()
    {
        var getSkillModifierCommand = new GetSkillModifier(character!, Survival.Instance);
        var result = getSkillModifierCommand.Execute();

        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(1, result.Value);
    }

    [TestMethod]
    public void TestGetSkillModifierWithoutProficiencyWithDisadvantage()
    {
        var getSkillModifierCommand = new GetSkillModifier(character!, Stealth.Instance);
        var result = getSkillModifierCommand.Execute();

        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(0, result.Value);
        Assert.IsTrue(result.BonusCollection.Advantage.HasDisadvantage());
    }
}
