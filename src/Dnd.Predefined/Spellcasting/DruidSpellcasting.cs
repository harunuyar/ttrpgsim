namespace Dnd.Predefined.Spellcasting;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.AbilityScore;
using Dnd._5eSRD.Models.Class;
using Dnd.Context;
using Dnd.Predefined.Commands.ScoreCommands;
using Dnd.System.Entities.GameActor;

public class DruidSpellcasting : ASpellcastingAbility
{
    private static readonly int[] MaxCantrips = [2, 2, 2, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4];
    private static readonly int[][] SpellSlotsPerLevel =[
            [2, 0, 0, 0, 0, 0, 0, 0, 0],
            [3, 0, 0, 0, 0, 0, 0, 0, 0],
            [4, 2, 0, 0, 0, 0, 0, 0, 0],
            [4, 3, 0, 0, 0, 0, 0, 0, 0],
            [4, 3, 2, 0, 0, 0, 0, 0, 0],
            [4, 3, 3, 0, 0, 0, 0, 0, 0],
            [4, 3, 3, 1, 0, 0, 0, 0, 0],
            [4, 3, 3, 2, 0, 0, 0, 0, 0],
            [4, 3, 3, 3, 1, 0, 0, 0, 0],
            [4, 3, 3, 3, 2, 0, 0, 0, 0],
            [4, 3, 3, 3, 2, 1, 0, 0, 0],
            [4, 3, 3, 3, 2, 1, 0, 0, 0],
            [4, 3, 3, 3, 2, 1, 1, 0, 0],
            [4, 3, 3, 3, 2, 1, 1, 0, 0],
            [4, 3, 3, 3, 2, 1, 1, 1, 0],
            [4, 3, 3, 3, 2, 1, 1, 1, 0],
            [4, 3, 3, 3, 2, 1, 1, 1, 1],
            [4, 3, 3, 3, 3, 1, 1, 1, 1],
            [4, 3, 3, 3, 3, 2, 1, 1, 1],
            [4, 3, 3, 3, 3, 2, 2, 1, 1]
        ];

    public static async Task<DruidSpellcasting> Create()
    {
        var classModel = await DndContext.Instance.GetObject<ClassModel>(Classes.Druid);
        return classModel == null 
            ? throw new InvalidOperationException("Druid class model is not found") 
            : new DruidSpellcasting(classModel);
    }

    private DruidSpellcasting(ClassModel classModel) : base(classModel, null)
    {
    }

    public override Task<int> GetMaxCantripsKnown(IGameActor gameActor)
    {
        int levelsInClass = gameActor.LevelInfo.GetLevelsInClass(ClassModel);
        return levelsInClass != 0 ? Task.FromResult(MaxCantrips[levelsInClass - 1]) : Task.FromResult(0);
    }

    public override async Task<int> GetMaxSpellsKnown(IGameActor gameActor)
    {
        var wisdom = await DndContext.Instance.GetObject<AbilityScoreModel>(AbilityScores.Wis);
        if (wisdom == null)
        {
            throw new InvalidOperationException("Wisdom ability score model is not found");
        }

        var wisdomModifier = await new GetAbilityModifier(gameActor, wisdom).Execute();

        if (!wisdomModifier.IsSuccess)
        {
            throw new InvalidOperationException("GetAbilityModifier: " + wisdomModifier.ErrorMessage);
        }

        int levelsInClass = gameActor.LevelInfo.GetLevelsInClass(ClassModel);
        return Math.Max(levelsInClass != 0 ? levelsInClass + wisdomModifier.Value : 0, 1);
    }

    public override Task<int> GetMaxSpellSlots(IGameActor gameActor, int spellLevel)
    {
        int levelsInClass = gameActor.LevelInfo.GetLevelsInClass(ClassModel);
        return levelsInClass != 0 ? Task.FromResult(SpellSlotsPerLevel[levelsInClass - 1][spellLevel - 1]) : Task.FromResult(0);
    }
}
