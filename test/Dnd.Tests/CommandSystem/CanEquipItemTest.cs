namespace Dnd.Tests.CommandSystem;

using Dnd.Predefined.Alignments;
using Dnd.Predefined.Armors.HeavyArmor;
using Dnd.Predefined.Characters;
using Dnd.Predefined.Races;
using Dnd.System.CommandSystem.Commands.BooleanResultCommands;
using Dnd.System.Entities.Characters;
using Dnd.System.Entities.Items;

[TestClass]
public class CanEquipItemTest
{
    private ICharacter? character;
    private IItem? armor;

    [TestInitialize]
    public void TestInitialize()
    {
        character = new CustomCharacter("Test", Human.Instance, ChaoticGood.Instance);
        armor = new Item(ChainMailArmor.Instance);
    }

    [TestMethod]
    public void TestCanEquipItemTrue()
    {
        character!.AttributeSet.Strength.Score = 15; // +1 from Human

        var canEquipItemCommand = new CanEquipItem(character, armor!);
        var result = canEquipItemCommand.Execute();

        Assert.IsTrue(result.IsSuccess);
        Assert.IsTrue(result.Value);
    }

    [TestMethod]
    public void TestCanEquipItemAlreadyEquiped()
    {
        character!.AttributeSet.Strength.Score = 15; // +1 from Human

        character.Inventory.EquipArmor(armor!);

        var canEquipItemCommand = new CanEquipItem(character, armor!);
        var result = canEquipItemCommand.Execute();

        Assert.IsTrue(result.IsSuccess);
        Assert.IsFalse(result.Value);
    }

    [TestMethod]
    public void TestCanEquipItemNotEnoughStrength()
    {
        character!.AttributeSet.Strength.Score = 1; // +1 from Human

        var canEquipItemCommand = new CanEquipItem(character, armor!);
        var result = canEquipItemCommand.Execute();

        Assert.IsTrue(result.IsSuccess);
        Assert.IsFalse(result.Value);
    }
}
