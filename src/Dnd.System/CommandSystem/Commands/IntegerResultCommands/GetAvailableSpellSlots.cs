namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.Entities.GameActors;

public class GetAvailableSpellSlots : DndScoreCommand
{
    public GetAvailableSpellSlots(IGameActor character, int spellLevel) : base(character)
    {
        SpellLevel = spellLevel;
    }

    public int SpellLevel { get; }

    protected override void InitializeResult()
    {
        if (SpellLevel < 1 || SpellLevel > 9)
        {
            SetErrorAndReturn("Spell level must be between 1 and 9");
            return;
        }

        Result.SetBaseValue("Base", 0);
    }
}
