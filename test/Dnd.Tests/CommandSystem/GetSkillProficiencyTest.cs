namespace Dnd.Tests.CommandSystem;

using Dnd.Predefined.Characters;
using Dnd.Predefined.Classes;
using Dnd.Predefined.Feats.Classes.Fighter.Level1.FightingStyle;
using Dnd.Predefined.Levels.Fighter;
using Dnd.Predefined.Races;
using Dnd.Predefined.Skills;
using Dnd.System.CommandSystem.Commands.BooleanResultCommands;
using Dnd.System.Entities.GameActors;

[TestClass]
public class GetSkillProficiencyTest
{
    private IGameActor? character;

    [TestInitialize]
    public void TestInitialize()
    {
        character = new CustomCharacter("Test", new Dragonborn());
        character.AttributeSet.Set(strength: 15, dexterity: 10, constitution: 14, intelligence: 8, wisdom: 12, charisma: 13);

        character.LevelInfo.AddLevel(new FighterLevel1(new Fighter(), Athletics.Instance, Intimidation.Instance, Defense.Instance));
    }

    [TestMethod]
    public void TestGetSkillProficiencyTrue()
    {
        var getSkillProficiencyCommand = new HasSkillProficiency(character!, Intimidation.Instance);
        var result = getSkillProficiencyCommand.Execute();

        Assert.IsTrue(result.IsSuccess);
        Assert.IsTrue(result.Value);
    }

    [TestMethod]
    public void TestGetSkillProficiencyFalse()
    {
        var getSkillProficiencyCommand = new HasSkillProficiency(character!, Perception.Instance);
        var result = getSkillProficiencyCommand.Execute();

        Assert.IsTrue(result.IsSuccess);
        Assert.IsFalse(result.Value);
    }
}
