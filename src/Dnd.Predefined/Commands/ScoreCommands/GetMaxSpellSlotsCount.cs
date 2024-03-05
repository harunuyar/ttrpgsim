namespace Dnd.Predefined.Commands.ScoreCommands;

using Dnd.Predefined.ModelExtensions;
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

        if (Actor.IsMulticlassSpellcaster())
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
