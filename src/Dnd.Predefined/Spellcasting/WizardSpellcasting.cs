namespace Dnd.Predefined.Spellcasting;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.AbilityScore;
using Dnd._5eSRD.Models.Class;
using Dnd.Context;
using Dnd.Predefined.Commands.ScoreCommands;
using Dnd.System.Entities.GameActor;

public class WizardSpellcasting : ASpellcastingAbility
{
    private static readonly int[] MaxCantrips = [3, 3, 3, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5];
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

    public static async Task<WizardSpellcasting> Create()
    {
        var classModel = await DndContext.Instance.GetObject<ClassModel>(Classes.Wizard);
        return classModel == null 
            ? throw new InvalidOperationException("Wizard class model is not found") 
            : new WizardSpellcasting(classModel);
    }

    private WizardSpellcasting(ClassModel classModel) : base(classModel, null)
    {
    }

    public override Task<int> GetMaxCantripsKnown(IGameActor gameActor)
    {
        int levelsInClass = gameActor.LevelInfo.GetLevelsInClass(ClassModel);
        return levelsInClass != 0 ? Task.FromResult(MaxCantrips[levelsInClass - 1]) : Task.FromResult(0);
    }

    public override async Task<int> GetMaxSpellsKnown(IGameActor gameActor)
    {
        var intelligence = await DndContext.Instance.GetObject<AbilityScoreModel>(AbilityScores.Int);
        if (intelligence == null)
        {
            throw new InvalidOperationException("Intelligence ability score model is not found");
        }

        var intelligenceModifier = await new GetAbilityModifier(gameActor, intelligence).Execute();

        if (!intelligenceModifier.IsSuccess)
        {
            throw new InvalidOperationException("GetAbilityModifier: " + intelligenceModifier.ErrorMessage);
        }

        int levelsInClass = gameActor.LevelInfo.GetLevelsInClass(ClassModel);
        return Math.Max(levelsInClass != 0 ? levelsInClass + intelligenceModifier.Value : 0, 1);
    }

    public override Task<int> GetMaxSpellSlots(IGameActor gameActor, int spellLevel)
    {
        int levelsInClass = gameActor.LevelInfo.GetLevelsInClass(ClassModel);
        return levelsInClass != 0 ? Task.FromResult(SpellSlotsPerLevel[levelsInClass - 1][spellLevel - 1]) : Task.FromResult(0);
    }
}
