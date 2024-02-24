namespace Dnd.Predefined.Commands.ScoreCommands;

using Dnd._5eSRD.Models.Class;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class GetMaxKnownSpellsCount : ScoreCommand
{
    public GetMaxKnownSpellsCount(IGameActor character, ClassModel classModel) : base(character)
    {
        ClassModel = classModel;
    }

    public ClassModel ClassModel { get; }

    protected override Task InitializeResult()
    {
        SetBaseValue(0, "Default");

        var level = Actor.LevelInfo.GetLevelForClass(ClassModel);

        if (level is null)
        {
            return Task.CompletedTask;
        }

        int? spellsKnown = level?.LevelModel?.Spellcasting?.SpellsKnown;
        if (spellsKnown is not null)
        {
            SetBaseValue(spellsKnown.Value, level!.LevelModel.Name ?? "Spellcaster Level");
        }
        
        // Some classes might have different rules for spells known. That will be handled by features.

        return Task.CompletedTask;
    }
}
