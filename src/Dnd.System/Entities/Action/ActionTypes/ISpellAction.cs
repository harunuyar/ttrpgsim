namespace Dnd.System.Entities.Action.ActionTypes;

using Dnd._5eSRD.Models.Spell;

public interface ISpellAction : IAction
{
    SpellModel Spell { get; }
    bool Concentration { get; }
}
