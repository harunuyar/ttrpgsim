namespace Dnd.System.Entities.Action.ActionTypes;

using Dnd._5eSRD.Models.Spell;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.Instances;

public interface ISpellAction : IAction
{
    SpellModel Spell { get; }
    ISpellcastingAbility SpellcastingAbility { get; }
    int SpellSlot { get; }
}
