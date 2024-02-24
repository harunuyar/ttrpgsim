namespace Dnd.Predefined.Commands.ScoreCommands;

using Dnd._5eSRD.Models.Class;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class GetMaxKnownCantripsCount : ScoreCommand
{
    public GetMaxKnownCantripsCount(IGameActor character, ClassModel classModel) : base(character)
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

        int? cantripsKnown = level?.LevelModel?.Spellcasting?.CantripsKnown;
        if (cantripsKnown is not null)
        {
            SetBaseValue(cantripsKnown.Value, level!.LevelModel.Name ?? "Spellcaster Level");
        }

        return Task.CompletedTask;
    }
}
