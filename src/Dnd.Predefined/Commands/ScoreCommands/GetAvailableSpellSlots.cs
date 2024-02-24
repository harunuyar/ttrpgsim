namespace Dnd.Predefined.Commands.ScoreCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class GetAvailableSpellSlots : ScoreCommand
{
    public GetAvailableSpellSlots(IGameActor character, int spellLevel) : base(character)
    {
        SpellLevel = spellLevel;
    }

    public int SpellLevel { get; }

    protected override async Task InitializeResult()
    {
        if (SpellLevel < 1 || SpellLevel > 9)
        {
            SetError("Spell level must be between 1 and 9");
            return;
        }

        var maxSlots = await new GetMaxSpellSlotsCount(Actor, SpellLevel).Execute();

        if (!maxSlots.IsSuccess)
        {
            SetError("GetMaxSpellSlotsCount: " + maxSlots.ErrorMessage);
            return;
        }

        SetBaseValue(maxSlots.Value - Actor.SpellMemory.GetUsedSpellSlots(SpellLevel), "Available Spell Slots");
    }
}
