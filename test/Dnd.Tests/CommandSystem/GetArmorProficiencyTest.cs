namespace Dnd.Tests.CommandSystem;

using Dnd.CommandSystem.Commands.BooleanResultCommands;
using Dnd.Entities.Characters;
using Dnd.Entities.Classes.Predefined;
using Dnd.Entities.Items.Equipments.Armors;
using Dnd.Entities.Races.Predefined;

[TestClass]
public class GetArmorProficiencyTest
{
    private Character? character;

    [TestInitialize]
    public void TestInitialize()
    {
        character = new Character("Test", Human.Instance);
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
        var getArmorProficiencyCommand = new GetArmorProficiency(character!, armorType);
        var result = getArmorProficiencyCommand.Execute();

        Assert.IsTrue(result.IsSuccess);
        Assert.IsTrue(proficiency);
    }
}
