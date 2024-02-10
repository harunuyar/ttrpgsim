namespace Dnd.Tests.CommandSystem;

using Dnd.CommandSystem.Commands.IntegerResultCommands;
using Dnd.Entities.Attributes;
using Dnd.Entities.Characters;
using Dnd.Entities.Classes.Predefined;
using Dnd.Entities.Races;
using Dnd.Entities.Skills.Predefined;

[TestClass]
public class CommandSystemTest
{
    private Character? character;

    [TestInitialize]
    public void TestInitialize()
    {
        character = new Character("Cengiz", Race.Human);

        character.AttributeSet.Set(strength: 15, dexterity: 10, constitution: 14, intelligence: 8, wisdom: 12, charisma: 13);

        character.Levels.Add(new Level(Fighter.Instance, 1));

        character.SetSkillProficiency(Intimidation.Instance, true);
        character.SetSkillProficiency(Athletics.Instance, true);
    }

    [TestMethod]
    public void TestGetAttributeScore()
    {
        var getAttributeScoreCommand = new GetAttributeScore(character!, EAttributeType.Strength);
        var result = getAttributeScoreCommand.Execute();

        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(15, result.Value);
    }
}