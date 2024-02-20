namespace Dnd.Tests._5eSRD;

using Dnd._5eSRD;
using Dnd._5eSRD.Models.AbilityScore;

[TestClass]
public class DndContextTest
{
    [TestMethod]
    public void DndContext_GetObject_AbilityScore_NotNull()
    {
        Assert.IsNotNull(DndContext.Instance.GetObject<AbilityScore>(Dnd._5eSRD.Constants.AbilityScores.Con).Result);
    }

    [TestMethod]
    public void DndContext_GetObject_AbilityScore_Null()
    {
        Assert.IsNull(DndContext.Instance.GetObject<AbilityScore>("").Result);
    }
}
