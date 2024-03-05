namespace Dnd.Predefined.Spellcasting;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.AbilityScore;
using Dnd._5eSRD.Models.Class;
using Dnd.Context;
using Dnd.Predefined.Commands.ScoreCommands;
using Dnd.System.Entities.GameActor;

public class PaladinSpellcasting : ASpellcastingAbility
{
    private static readonly int[] MaxSpells = [0, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 11, 11];
    private static readonly int[][] SpellSlotsPerLevel =[
            [0, 0, 0, 0, 0, 0, 0, 0, 0],
            [2, 0, 0, 0, 0, 0, 0, 0, 0],
            [3, 0, 0, 0, 0, 0, 0, 0, 0],
            [3, 0, 0, 0, 0, 0, 0, 0, 0],
            [4, 2, 0, 0, 0, 0, 0, 0, 0],
            [4, 2, 0, 0, 0, 0, 0, 0, 0],
            [4, 3, 0, 0, 0, 0, 0, 0, 0],
            [4, 3, 0, 0, 0, 0, 0, 0, 0],
            [4, 3, 2, 0, 0, 0, 0, 0, 0],
            [4, 3, 2, 0, 0, 0, 0, 0, 0],
            [4, 3, 3, 0, 0, 0, 0, 0, 0],
            [4, 3, 3, 0, 0, 0, 0, 0, 0],
            [4, 3, 3, 1, 0, 0, 0, 0, 0],
            [4, 3, 3, 1, 0, 0, 0, 0, 0],
            [4, 3, 3, 2, 0, 0, 0, 0, 0],
            [4, 3, 3, 2, 0, 0, 0, 0, 0],
            [4, 3, 3, 3, 1, 0, 0, 0, 0],
            [4, 3, 3, 3, 1, 0, 0, 0, 0],
            [4, 3, 3, 3, 2, 0, 0, 0, 0],
            [4, 3, 3, 3, 2, 0, 0, 0, 0]
        ];

    public static async Task<PaladinSpellcasting> Create()
    {
        var classModel = await DndContext.Instance.GetObject<ClassModel>(Classes.Paladin);
        return classModel == null 
            ? throw new InvalidOperationException("Paladin class model is not found") 
            : new PaladinSpellcasting(classModel);
    }

    private PaladinSpellcasting(ClassModel classModel) : base(classModel, null)
    {
    }

    public override Task<int> GetMaxCantripsKnown(IGameActor gameActor)
    {
        return Task.FromResult(0);
    }

    public override async Task<int> GetMaxSpellsKnown(IGameActor gameActor)
    {
        var charisma = await DndContext.Instance.GetObject<AbilityScoreModel>(AbilityScores.Cha);
        if (charisma == null)
        {
            throw new InvalidOperationException("Charisma ability score model is not found");
        }

        var charismaModifier = await new GetAbilityModifier(gameActor, charisma).Execute();

        if (!charismaModifier.IsSuccess)
        {
            throw new InvalidOperationException("GetAbilityModifier: " + charismaModifier.ErrorMessage);
        }

        int levelsInClass = gameActor.LevelInfo.GetLevelsInClass(ClassModel);
        return Math.Max(levelsInClass != 0 ? (levelsInClass / 2) + charismaModifier.Value : 0, 1);
    }

    public override Task<int> GetMaxSpellSlots(IGameActor gameActor, int spellLevel)
    {
        int levelsInClass = gameActor.LevelInfo.GetLevelsInClass(ClassModel);
        return levelsInClass == 0
            ? Task.FromResult(0)
            : Task.FromResult(SpellSlotsPerLevel[levelsInClass - 1][spellLevel - 1]);
    }
}
