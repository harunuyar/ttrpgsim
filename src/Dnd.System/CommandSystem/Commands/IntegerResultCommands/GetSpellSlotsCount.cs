namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.Entities.GameActors;

public class GetSpellSlotsCount : DndScoreCommand
{
    public GetSpellSlotsCount(IGameActor character, int spellLevel) : base(character)
    {
        SpellLevel = spellLevel;
    }

    public int SpellLevel { get; }

    protected override void FinalizeResult()
    {
    }

    protected override void InitializeResult()
    {
        Result.SetBaseValue("Default", 0);
    }
}
