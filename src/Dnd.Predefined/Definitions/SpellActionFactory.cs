namespace Dnd.Predefined.Definitions;

using Dnd._5eSRD.Models.Spell;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;
using Dnd.System.Entities.Instances;

public class SpellActionFactory
{
    public static Task<List<ISpellAction>> CreateSpellAction(IGameActor actionOwner, ISpellcastingAbility spellcastingAbility, SpellModel spellModel)
    {
        throw new NotImplementedException();
    }
}
