namespace Dnd.Tests.CommandSystem;

using Dnd.Predefined.Alignments;
using Dnd.Predefined.Armors.HeavyArmor;
using Dnd.Predefined.Armors.Shield;
using Dnd.Predefined.Characters;
using Dnd.Predefined.Feats.Fighter.Level1.FightingStyle;
using Dnd.Predefined.Levels.FighterLevels;
using Dnd.Predefined.Races;
using Dnd.Predefined.Skills;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Items;

[TestClass]
public class GetArmorClassTest
{
    private IGameActor? character;

    [TestInitialize]
    public void TestInitialize()
    {
        character = new CustomCharacter("Test", Human.Instance, ChaoticGood.Instance);

        character.AttributeSet.Set(strength: 15, dexterity: 10, constitution: 14, intelligence: 8, wisdom: 12, charisma: 13);

        character.LevelInfo.AddLevel(new FighterLevel1(Athletics.Instance, Intimidation.Instance, Defense.Instance));

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