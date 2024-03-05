namespace Dnd.Predefined.Spellcasting;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.Class;
using Dnd.Context;
using Dnd.System.Entities.GameActor;

public class WarlockSpellcasting : ASpellcastingAbility
{
    private static readonly int[] MaxCantrips = [2, 2, 2, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4];
    private static readonly int[] MaxSpells = [2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 11, 11, 12, 12, 13, 13, 14, 14, 15, 15];
    private static readonly int[][] SpellSlotsPerLevel =[
            [1, 0, 0, 0, 0, 0, 0, 0, 0],
            [2, 0, 0, 0, 0, 0, 0, 0, 0],
            [0, 2, 0, 0, 0, 0, 0, 0, 0],
            [0, 2, 0, 0, 0, 0, 0, 0, 0],
            [0, 0, 2, 0, 0, 0, 0, 0, 0],
            [0, 0, 2, 0, 0, 0, 0, 0, 0],
            [0, 0, 0, 2, 0, 0, 0, 0, 0],
            [0, 0, 0, 2, 0, 0, 0, 0, 0],
            [0, 0, 0, 0, 2, 0, 0, 0, 0],
            [0, 0, 0, 0, 2, 0, 0, 0, 0],
            [0, 0, 0, 0, 3, 0, 0, 0, 0],
            [0, 0, 0, 0, 3, 0, 0, 0, 0],
            [0, 0, 0, 0, 3, 0, 0, 0, 0],
            [0, 0, 0, 0, 3, 0, 0, 0, 0],
            [0, 0, 0, 0, 3, 0, 0, 0, 0],
            [0, 0, 0, 0, 3, 0, 0, 0, 0],
            [0, 0, 0, 0, 4, 0, 0, 0, 0],
            [0, 0, 0, 0, 4, 0, 0, 0, 0],
            [0, 0, 0, 0, 4, 0, 0, 0, 0],
            [0, 0, 0, 0, 4, 0, 0, 0, 0]
        ];

    public static async Task<WarlockSpellcasting> Create()
    {
        var classModel = await DndContext.Instance.GetObject<ClassModel>(Classes.Warlock);
        return classModel == null 
            ? throw new InvalidOperationException("Warlock class model is not found") 
            : new WarlockSpellcasting(classModel);
    }

    private WarlockSpellcasting(ClassModel classModel) : base(classModel, null)
    {
    }

    public override Task<int> GetMaxCantripsKnown(IGameActor gameActor)
    {
        int levelsInClass = gameActor.LevelInfo.GetLevelsInClass(ClassModel);
        return levelsInClass == 0
            ? Task.FromResult(0)
            : Task.FromResult(MaxCantrips[levelsInClass - 1]);
    }

    public override Task<int> GetMaxSpellsKnown(IGameActor gameActor)
    {
        int levelsInClass = gameActor.LevelInfo.GetLevelsInClass(ClassModel);
        return Task.FromResult(Math.Max(levelsInClass == 0 ? 0 : MaxSpells[levelsInClass - 1], 1));
    }

    public override Task<int> GetMaxSpellSlots(IGameActor gameActor, int spellLevel)
    {
        int levelsInClass = gameActor.LevelInfo.GetLevelsInClass(ClassModel);
        return levelsInClass == 0
            ? Task.FromResult(0)
            : Task.FromResult(SpellSlotsPerLevel[levelsInClass - 1][spellLevel - 1]);
    }
}
