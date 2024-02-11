namespace Dnd.Tests.CommandSystem;

using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
using Dnd.System.Entities.Advantage;
using Dnd.System.Entities.Characters;
using Dnd.Predefined.Characters;
using Dnd.Predefined.Races;
using Dnd.Predefined.Classes;
using Dnd.Predefined.Skills;
using Dnd.Predefined.Armors.HeavyArmor;
using Dnd.System.Entities.Items;

[TestClass]
public class GetSkillModifierTest
{
    private ICharacter? character;

    [TestInitialize]
    public void TestInitialize()
    {
        character = new CustomCharacter("Test", Dragonborn.Instance); // strength +2, charisma +1
        character.AttributeSet.Set(strength: 15, dexterity: 10, constitution: 14, intelligence: 8, wisdom: 12, charisma: 13);

        character.Levels.Add(new Level(Fighter.Instance, 1));

        //character.SetSkillProficiency(Intimidation.Instance, true);
        //character.SetSkillProficiency(Athletics.Instance, true);

        character.Inventory.EquipArmor(new Item(ChainMailArmor.Instance));
    }

    [TestMethod]
    [Ignore("Not implemented")]
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
