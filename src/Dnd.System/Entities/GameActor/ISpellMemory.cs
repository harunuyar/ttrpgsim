namespace Dnd.System.Entities.GameActor;

using Dnd._5eSRD.Models.Class;
using Dnd._5eSRD.Models.Spell;
using Dnd.System.Entities.Action.ActionTypes;

public interface ISpellMemory : ICommandHandler
{
    int GetUsedSpellSlots(int level);
    void UseSpellSlot(int level);
    void ResetSpellSlots();
    void PrepareSpell(ClassModel classModel, ISpellAction spell);
    void UnprepareSpell(ClassModel classModel, ISpellAction spell);
    void LearnCantrip(ClassModel classModel, ISpellAction spell);
    void ForgetCantrip(ClassModel classModel, ISpellAction spell);
    List<ISpellAction> GetCantrips();
    List<ISpellAction> GetPreparedSpells();
    List<ISpellAction> GetCantrips(ClassModel classModel);
    List<ISpellAction> GetPreparedSpells(ClassModel classModel);
    bool HasPreparedSpell(SpellModel spell);
    bool HasCantrip(SpellModel spell);
}
