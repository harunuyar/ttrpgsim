namespace Dnd.Tests.CommandSystem;

using Dnd.Predefined.Characters;
using Dnd.Predefined.Classes;
using Dnd.Predefined.Feats.Classes.Fighter.Level1.FightingStyle;
using Dnd.Predefined.Levels.FighterLevels;
using Dnd.Predefined.Races;
using Dnd.Predefined.Skills;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.Entities.GameActors;

[TestClass]
public class GetMaxHPTest
{
    [TestMethod]
    public void TestGetMaxHitPoints()
    {
        IGameActor character = new CustomCharacter("Test", Human.Instance);
        character.AttributeSet.Constitution.Score = 14; // +1 from human racial bonus

        character.LevelInfo.AddLevel(new FighterLevel1(Athletics.Instance, Intimidation.Instance, Defense.Instance));
        character.HitPoints.AddHitPointRoll((int)Fighter.Instance.HitDie);

        var getMaxHitPointsCommand = new GetMaxHP(character!);
        var result = getMaxHitPointsCommand.Execute();

        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(12, result.Value); // 10 (first roll) + 1 (level) * 2 (constitution modifier)

        character.AttributeSet.Constitution.Score = 16; // +1 from human racial bonus

        result = getMaxHitPointsCommand.Execute();

        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(13, result.Value); // 10 (first roll) + 1 (level) * 3 (constitution modifier)
    }
}
