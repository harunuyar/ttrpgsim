namespace Dnd.Tests.CommandSystem;

using Dnd.Predefined.Alignments;
using Dnd.Predefined.Characters;
using Dnd.Predefined.Feats.Classes.Fighter.Level1.FightingStyle;
using Dnd.Predefined.Levels.FighterLevels;
using Dnd.Predefined.Races;
using Dnd.Predefined.Skills;
using Dnd.System.CommandSystem.Commands.BooleanResultCommands;
using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Items.Equipments.Armors;

[TestClass]
public class GetArmorProficiencyTest
{
    private IGameActor? character;

    [TestInitialize]
    public void TestInitialize()
    {
        character = new CustomCharacter("Test", Human.Instance, ChaoticGood.Instance);
        character.LevelInfo.AddLevel(new FighterLevel1(Athletics.Instance, Intimidation.Instance, Defense.Instance));
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
