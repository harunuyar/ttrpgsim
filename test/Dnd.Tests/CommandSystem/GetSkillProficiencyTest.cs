namespace Dnd.Tests.CommandSystem;

using Dnd.Predefined.Characters;
using Dnd.Predefined.Races;
using Dnd.Predefined.Skills;
using Dnd.System.CommandSystem.Commands.BooleanResultCommands;
using Dnd.System.Entities.Characters;

[TestClass]
public class GetSkillProficiencyTest
{
    private ICharacter? character;

    [TestInitialize]
    public void TestInitialize()
    {
        character = new CustomCharacter("Test", Dragonborn.Instance);

        // character.SetSkillProficiency(Intimidation.Instance, true);
        // character.SetSkillProficiency(Athletics.Instance, true);
    }

    [TestMethod]
    [Ignore("Not implemented")]
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
