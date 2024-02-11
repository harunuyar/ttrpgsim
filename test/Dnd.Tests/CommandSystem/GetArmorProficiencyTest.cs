namespace Dnd.Tests.CommandSystem;

using Dnd.Predefined.Alignments;
using Dnd.Predefined.Characters;
using Dnd.Predefined.Classes;
using Dnd.Predefined.Races;
using Dnd.System.CommandSystem.Commands.BooleanResultCommands;
using Dnd.System.Entities.Characters;
using Dnd.System.Entities.Items.Equipments.Armors;

[TestClass]
public class GetArmorProficiencyTest
{
    private ICharacter? character;

    [TestInitialize]
    public void TestInitialize()
    {
        character = new CustomCharacter("Test", Human.Instance, ChaoticGood.Instance);
        character.Levels.Add(new Level(Fighter.Instance, 2));
    }

    [TestMethod]
    [DataRow(EArmorType.Light, true)]
    [DataRow(EArmorType.Medium, true)]
    [DataRow(EArmorType.Heavy, true)]
    [DataRow(EArmorType.Shield, true)]
    [DataRow(EArmorType.All, true)]
    public void TestGetArmorProficiency(EArmorType armorType, bool proficiency)
    {
        var getArmorProficiencyCommand = new HasArmorProficiency(character!, armorType);
        var result = getArmorProficiencyCommand.Execute();

        Assert.IsTrue(result.IsSuccess);
        Assert.IsTrue(proficiency);
    }
}
