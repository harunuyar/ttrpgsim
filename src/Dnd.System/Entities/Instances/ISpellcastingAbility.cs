namespace Dnd.System.Entities.Instances;

using Dnd._5eSRD.Models.Class;
using Dnd._5eSRD.Models.Spell;
using Dnd._5eSRD.Models.Subclass;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;

public interface ISpellcastingAbility  : ICommandHandler, IUsageBonusProvider
{
    ClassModel ClassModel { get; }
    SubclassModel? SubclassModel { get; }
    Task<int> GetMaxCantripsKnown(IGameActor gameActor);
    Task<int> GetMaxSpellsKnown(IGameActor gameActor);
    Task<int> GetMaxSpellSlots(IGameActor gameActor, int spellLevel);
    List<ISpellAction> GetCantripActions();
    List<ISpellAction> GetSpellActions();
    void LearnSpell(ISpellAction spellAction);
    void UnlearnSpell(SpellModel spell);
    bool HasSpell(SpellModel spell, int spellSlot);
}
