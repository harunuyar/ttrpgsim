namespace Dnd.Tests.CommandSystem;

using Dnd.CommandSystem.Commands.BooleanResultCommands;
using Dnd.Entities.Characters;
using Dnd.Entities.Races.Predefined;
using Dnd.Entities.Skills.Predefined;

[TestClass]
public class GetSkillProficiencyTest
{
    private Character? character;

    [TestInitialize]
    public void TestInitialize()
    {
        character = new Character("Test", Dragonborn.Instance);

        character.SetSkillProficiency(Intimidation.Instance, true);
        character.SetSkillProficiency(Athletics.Instance, true);
    }

    [TestMethod]
    public void TestGetSkillProficiencyTrue()
    {
        var getSkillProficiencyCommand = new GetSkillProficiency(character!, Intimidation.Instance);
        var result = getSkillProficiencyCommand.Execute();

        Assert.IsTrue(result.IsSuccess);
        Assert.IsTrue(result.Value);
    }

    [TestMethod]
    public void TestGetSkillProficiencyFalse()
    {
        var getSkillProficiencyCommand = new GetSkillProficiency(character!, Perception.Instance);
        var result = getSkillProficiencyCommand.Execute();

        Assert.IsTrue(result.IsSuccess);
        Assert.IsFalse(result.Value);
    }
}
