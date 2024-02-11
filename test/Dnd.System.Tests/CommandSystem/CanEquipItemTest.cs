namespace Dnd.Tests.CommandSystem;

using Dnd.CommandSystem.Commands.BooleanResultCommands;
using Dnd.Entities.Characters;
using Dnd.Entities.Items;
using Dnd.Entities.Items.Equipments.Armors.HeavyArmor;
using Dnd.Entities.Races.Predefined;

[TestClass]
public class CanEquipItemTest
{
    [TestMethod]
    public void TestCanEquipItemTrue()
    {
        var character = new Character("Test", Human.Instance);
        character.AttributeSet.Strength.Score = 15; // +1 from Human

        var item = new Item(new ChainMailArmor());

        var canEquipItemCommand = new CanEquipItem(character, item);
        var result = canEquipItemCommand.Execute();

        Assert.IsTrue(result.IsSuccess);
        Assert.IsTrue(result.Value);
    }

    [TestMethod]
    public void TestCanEquipItemAlreadyEquiped()
    {
        var character = new Character("Test", Human.Instance);
        character.AttributeSet.Strength.Score = 15; // +1 from Human

        var item = new Item(new ChainMailArmor());
        item.IsEquipped = true;

        var canEquipItemCommand = new CanEquipItem(character, item);
        var result = canEquipItemCommand.Execute();

        Assert.IsTrue(result.IsSuccess);
        Assert.IsFalse(result.Value);
    }

    [TestMethod]
    public void TestCanEquipItemNotEnoughStrength()
    {
        var character = new Character("Test", Human.Instance);
        character.AttributeSet.Strength.Score = 1; // +1 from Human

        var item = new Item(new ChainMailArmor());

        var canEquipItemCommand = new CanEquipItem(character, item);
        var result = canEquipItemCommand.Execute();

        Assert.IsTrue(result.IsSuccess);
        Assert.IsFalse(result.Value);
    }
}
