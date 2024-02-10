namespace Dnd.Tests.CommandSystem;

using Dnd.CommandSystem.Commands.IntegerResultCommands;
using Dnd.Entities.Characters;
using Dnd.Entities.Classes.Predefined;
using Dnd.Entities.Feats.Fighter;
using Dnd.Entities.Items;
using Dnd.Entities.Items.Equipments.Armors;
using Dnd.Entities.Items.Equipments.Armors.HeavyArmor;
using Dnd.Entities.Races.Predefined;

[TestClass]
public class GetArmorClassTest
{
    private Character? character;

    [TestInitialize]
    public void TestInitialize()
    {
        character = new Character("Test", Human.Instance);

        character.AttributeSet.Set(strength: 15, dexterity: 10, constitution: 14, intelligence: 8, wisdom: 12, charisma: 13);

        character.Levels.Add(new Level(Fighter.Instance, 2));

        character.Feats.Add(FightingStyleDefense.Instance);

        character.Inventory.EquipArmor(new Item(new ChainMailArmor()));
        character.Inventory.EquipShield(new Item(new Shield()));
    }

    [TestMethod]
    public void TestGetArmorClass()
    {
        var getArmorClassCommand = new GetArmorClass(character!);
        var result = getArmorClassCommand.Execute();

        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(19, result.Value); // 16 (Chain Mail) + 2 (Shield) + 1 (Fighting Style: Defense)
    }
}