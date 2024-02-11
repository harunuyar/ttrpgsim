namespace Dnd.Tests.CommandSystem;

using Dnd.CommandSystem.Commands.IntegerResultCommands;
using Dnd.Entities.Advantage;
using Dnd.Entities.Characters;
using Dnd.Entities.Classes.Predefined;
using Dnd.Entities.Items.Equipments.Armors.HeavyArmor;
using Dnd.Entities.Items;
using Dnd.Entities.Races.Predefined;
using Dnd.Entities.Skills.Predefined;

[TestClass]
public class GetSkillModifierTest
{
    private Character? character;

    [TestInitialize]
    public void TestInitialize()
    {
        character = new Character("Test", Dragonborn.Instance); // strength +2, charisma +1
        character.AttributeSet.Set(strength: 15, dexterity: 10, constitution: 14, intelligence: 8, wisdom: 12, charisma: 13);

        character.Levels.Add(new Level(Fighter.Instance, 1));

        character.SetSkillProficiency(Intimidation.Instance, true);
        character.SetSkillProficiency(Athletics.Instance, true);

        character.Inventory.EquipArmor(new Item(new ChainMailArmor())); // Will give disadvantage to Stealth
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
        var getSkillModifierCommand = new GetSkillModifier(character!, Stealth.Instance);
        var result = getSkillModifierCommand.Execute();

        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(0, result.Value);
        Assert.IsTrue(result.BonusCollection.Advantage.HasDisadvantage());
    }
}
