namespace Dnd.Tests.CommandSystem;

using Dnd.Predefined.Alignments;
using Dnd.Predefined.Armors.HeavyArmor;
using Dnd.Predefined.Armors.Shield;
using Dnd.Predefined.Characters;
using Dnd.Predefined.Classes;
using Dnd.Predefined.Feats.Fighter;
using Dnd.Predefined.Races;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
using Dnd.System.Entities.Characters;
using Dnd.System.Entities.Items;

[TestClass]
public class GetArmorClassTest
{
    private ICharacter? character;

    [TestInitialize]
    public void TestInitialize()
    {
        character = new CustomCharacter("Test", Human.Instance, ChaoticGood.Instance);

        character.AttributeSet.Set(strength: 15, dexterity: 10, constitution: 14, intelligence: 8, wisdom: 12, charisma: 13);

        character.Levels.Add(new Level(Fighter.Instance, 2));

        character.Feats.Add(FightingStyleDefense.Instance);

        character.Inventory.EquipArmor(new Item(ChainMailArmor.Instance));
        character.Inventory.EquipShield(new Item(Shield.Instance));
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