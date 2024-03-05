namespace Dnd.Predefined.Commands.ScoreCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;
using Dnd.System.Entities.Instances;

public class GetMaxKnownSpellsCount : ScoreCommand
{
    public GetMaxKnownSpellsCount(IGameActor character, ISpellcastingAbility spellcastingAbility) : base(character)
    {
        SpellcastingAbility = spellcastingAbility;
    }

    public ISpellcastingAbility SpellcastingAbility { get; }

    protected override async Task InitializeResult()
    {
        try
        {
            int maxSpells = await SpellcastingAbility.GetMaxSpellsKnown(Actor);
            SetBaseValue(maxSpells, "Spellcasting Ability");
        }
        catch (Exception ex)
        {
            SetError(ex.Message);
        }
    }
}
