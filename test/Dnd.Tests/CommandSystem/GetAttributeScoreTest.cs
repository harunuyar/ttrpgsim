namespace Dnd.Tests.CommandSystem;

using Dnd.Predefined.Characters;
using Dnd.Predefined.Races;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.GameActors;

[TestClass]
public class GetAttributeScoreTest
{
    [TestMethod]
    [DataRow(EAttributeType.Strength, 16)] // base score (15) + racial bonus (1)
    [DataRow(EAttributeType.Dexterity, 11)] // base score (10) + racial bonus (1)
    [DataRow(EAttributeType.Constitution, 15)] // base score (14) + racial bonus (1)
    [DataRow(EAttributeType.Intelligence, 9)] // base score (8) + racial bonus (1)
    [DataRow(EAttributeType.Wisdom, 13)] // base score (12) + racial bonus (1)
    [DataRow(EAttributeType.Charisma, 14)] // base score (13) + racial bonus (1)
    public void TestGetAttributeScoreHuman(EAttributeType attribute, int score)
    {
        IGameActor character = new CustomCharacter("Test", new Human());
        character.AttributeSet.Set(strength: 15, dexterity: 10, constitution: 14, intelligence: 8, wisdom: 12, charisma: 13);

        var getAttributeScoreCommand = new GetTotalAttributeScore(character!, attribute);
        var result = getAttributeScoreCommand.Execute();

        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(score, result.Value);
    }

    [TestMethod]
    [DataRow(EAttributeType.Strength, 17)] // base score (15) + racial bonus (2)
    [DataRow(EAttributeType.Dexterity, 10)] // base score (10)
    [DataRow(EAttributeType.Constitution, 14)] // base score (14)
    [DataRow(EAttributeType.Intelligence, 8)] // base score (8)
    [DataRow(EAttributeType.Wisdom, 12)] // base score (12)
    [DataRow(EAttributeType.Charisma, 14)] // base score (13) + racial bonus (1)
    public void TestGetAttributeScoreDragonborn(EAttributeType attribute, int score)
    {
        IGameActor character = new CustomCharacter("Test", new Dragonborn());
        character.AttributeSet.Set(strength: 15, dexterity: 10, constitution: 14, intelligence: 8, wisdom: 12, charisma: 13);

        var getAttributeScoreCommand = new GetTotalAttributeScore(character!, attribute);
        var result = getAttributeScoreCommand.Execute();

        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(score, result.Value);
    }
}
