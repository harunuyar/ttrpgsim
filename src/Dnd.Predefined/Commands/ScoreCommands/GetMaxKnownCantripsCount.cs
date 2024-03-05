namespace Dnd.Predefined.Commands.ScoreCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;
using Dnd.System.Entities.Instances;

public class GetMaxKnownCantripsCount : ScoreCommand
{
    public GetMaxKnownCantripsCount(IGameActor character, ISpellcastingAbility spellcastingAbility) : base(character)
    {
        SpellcastingAbility = spellcastingAbility;
    }

    public ISpellcastingAbility SpellcastingAbility { get; }

    protected override async Task InitializeResult()
    {
        try
        {
            int maxSpells = await SpellcastingAbility.GetMaxCantripsKnown(Actor);
            SetBaseValue(maxSpells, "Spellcasting Ability");
        }
        catch (Exception ex)
        {
            SetError(ex.Message);
        }
    }
}
