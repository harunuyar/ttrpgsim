namespace Dnd.System.Entities.Actions.BaseActions;

using Dnd.System.Entities.Spells;

public abstract class ASpellAction : AAction
{
    public ASpellAction(ISpell spell, List<UsageLimitation> usageLimitations) : base(spell.Name, spell.Description, spell.ActionType, spell.Range, usageLimitations)
    {
        Spell = spell;
    }

    public ISpell Spell { get; }
}
