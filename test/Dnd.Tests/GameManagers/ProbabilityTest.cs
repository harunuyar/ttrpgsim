namespace Dnd.Tests.GameManagers;

using Dnd.System.GameManagers.Dice;

[TestClass]
public class ProbabilityTest
{
    [TestMethod]
    [DataRow(1, EDiceType.d20, EAdvantage.None, 11, 50)]
    [DataRow(1, EDiceType.d20, EAdvantage.Advantage, 11, 75)]
    [DataRow(1, EDiceType.d20, EAdvantage.Disadvantage, 11, 75)]
    [DataRow(2, EDiceType.d20, EAdvantage.None, 11, 89)]
    [DataRow(2, EDiceType.d20, EAdvantage.None, 40, 0)]
    [DataRow(2, EDiceType.d20, EAdvantage.None, 38, 2)]
    [DataRow(2, EDiceType.d20, EAdvantage.None, 2, 100)]
    [DataRow(50, EDiceType.d20, EAdvantage.None, 525, 50)]
    [DataRow(4, EDiceType.d6, EAdvantage.None, 4, 100)]
    [DataRow(1000, EDiceType.d6, EAdvantage.None, 3500, 50)]

    public void TestForAttackRoll(int diceNumber, EDiceType diceType, EAdvantage advantage, int target, int expectedResult)
    {
        Assert.AreEqual(expectedResult, Probability.CustomRoll(new DiceRoll(diceNumber, diceType), advantage, target).ToPercentage());
    }
}
