namespace Dnd.Predefined.Commands.ScoreCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class GetMaxSpellSlotsCount : ScoreCommand
{
    public static readonly int[,] MulticlassSpellSlotsTable = new int[,]
    {
        { 2, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 3, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 4, 2, 0, 0, 0, 0, 0, 0, 0 },
        { 4, 3, 0, 0, 0, 0, 0, 0, 0 },
        { 4, 3, 2, 0, 0, 0, 0, 0, 0 },
        { 4, 3, 3, 0, 0, 0, 0, 0, 0 },
        { 4, 3, 3, 1, 0, 0, 0, 0, 0 },
        { 4, 3, 3, 2, 0, 0, 0, 0, 0 },
        { 4, 3, 3, 3, 1, 0, 0, 0, 0 },
        { 4, 3, 3, 3, 2, 0, 0, 0, 0 },
        { 4, 3, 3, 3, 2, 1, 0, 0, 0 },
        { 4, 3, 3, 3, 2, 1, 0, 0, 0 },
        { 4, 3, 3, 3, 2, 1, 1, 0, 0 },
        { 4, 3, 3, 3, 2, 1, 1, 0, 0 },
        { 4, 3, 3, 3, 2, 1, 1, 1, 0 },
        { 4, 3, 3, 3, 2, 1, 1, 1, 0 },
        { 4, 3, 3, 3, 2, 1, 1, 1, 1 },
        { 4, 3, 3, 3, 3, 1, 1, 1, 1 },
        { 4, 3, 3, 3, 3, 2, 1, 1, 1 },
        { 4, 3, 3, 3, 3, 2, 2, 1, 1 }
    };

    public GetMaxSpellSlotsCount(IGameActor character, int spellLevel) : base(character)
    {
        SpellLevel = spellLevel;
    }

    public int SpellLevel { get; }


    protected override Task InitializeResult()
    {
        SetBaseValue(0, "Default");

        if (Actor.LevelInfo.Level == 0)
        {
            return Task.CompletedTask;
        }

        var spellcastingClasses = Actor.LevelInfo.GetClasses()
            .Where(c => c.ClassModel.Spellcasting?.Level is not null)
            .ToList();

        if (spellcastingClasses.Count == 0)
        {
            return Task.CompletedTask;
        }

        if (spellcastingClasses.Count == 1)
        {
            var level = Actor.LevelInfo.GetLevelForClass(spellcastingClasses[0].ClassModel);

            int spellSlot = SpellLevel switch
            {
                1 => level?.LevelModel?.Spellcasting?.SpellSlotsLevel1 ?? 0,
                2 => level?.LevelModel?.Spellcasting?.SpellSlotsLevel2 ?? 0,
                3 => level?.LevelModel?.Spellcasting?.SpellSlotsLevel3 ?? 0,
                4 => level?.LevelModel?.Spellcasting?.SpellSlotsLevel4 ?? 0,
                5 => level?.LevelModel?.Spellcasting?.SpellSlotsLevel5 ?? 0,
                6 => level?.LevelModel?.Spellcasting?.SpellSlotsLevel6 ?? 0,
                7 => level?.LevelModel?.Spellcasting?.SpellSlotsLevel7 ?? 0,
                8 => level?.LevelModel?.Spellcasting?.SpellSlotsLevel8 ?? 0,
                9 => level?.LevelModel?.Spellcasting?.SpellSlotsLevel9 ?? 0,
                _ => 0
            };

            SetBaseValue(spellSlot, level?.LevelModel?.Name ?? "Spellcasting Level");
        }
        else
        {
            int spellcastingLevel = Actor.LevelInfo.GetClasses()
            .Select(c => c.ClassModel.Spellcasting?.Level is null
                ? 0
                : (Actor.LevelInfo.GetLevelsInClass(c.ClassModel) / c.ClassModel.Spellcasting.Level.Value))
            .Sum();

            if (spellcastingLevel < 1 || spellcastingLevel > 9)
            {
                SetError("Spellcasting level is wrong: " + spellcastingLevel);
                return Task.CompletedTask;
            }

            int spellSlot = MulticlassSpellSlotsTable[spellcastingLevel - 1, SpellLevel - 1];
            SetBaseValue(spellSlot, "Multiclass Spell Slots Table");
        }

        return Task.CompletedTask;
    }
}
