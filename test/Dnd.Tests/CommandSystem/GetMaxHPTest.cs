namespace Dnd.Tests.CommandSystem;
using Dnd.CommandSystem.Commands.IntegerResultCommands;

using Dnd.Entities.Characters;
using Dnd.Entities.Classes.Predefined;
using Dnd.Entities.Races.Predefined;

[TestClass]
public class GetMaxHPTest
{
    [TestMethod]
    public void TestGetMaxHitPoints()
    {
        Character character = new Character("Test", Human.Instance);
        character.AttributeSet.Constitution.Score = 14; // +1 from human racial bonus

        character.Levels.Add(new Level(Fighter.Instance, 2));
        character.HitPoints.AddHitPointRoll((int)Fighter.Instance.HitDie);
        character.HitPoints.AddHitPointRoll(7);

        var getMaxHitPointsCommand = new GetMaxHP(character!);
        var result = getMaxHitPointsCommand.Execute();

        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(21, result.Value); // 10 (first roll) + 7 (second roll) + 2 (level) * 2 (constitution modifier)

        character.AttributeSet.Constitution.Score = 16; // +1 from human racial bonus

        result = getMaxHitPointsCommand.Execute();

        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(23, result.Value); // 10 (first roll) + 7 (second roll) + 2 (level) * 3 (constitution modifier)

        character.Levels[0].LevelNum = 3;
        character.HitPoints.AddHitPointRoll(6);

        result = getMaxHitPointsCommand.Execute();

        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(32, result.Value); // 10 (first roll) + 7 (second roll) + 6 (third roll) + 3 (level) * 3 (constitution modifier)
    }
}
