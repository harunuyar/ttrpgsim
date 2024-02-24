namespace Dnd.System.Entities.Action;

using Dnd._5eSRD.Models.Spell;

public interface ISpellAction : IAction
{
    SpellModel Spell { get; }
}
