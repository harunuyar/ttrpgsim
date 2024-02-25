namespace Dnd.Predefined.GameActor;

using Dnd._5eSRD.Models.Class;
using Dnd._5eSRD.Models.Spell;
using Dnd.Predefined.Commands.BoolCommands;
using Dnd.Predefined.Commands.ListCommands;
using Dnd.Predefined.Commands.ScoreCommands;
using Dnd.Predefined.Commands.VoidCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;

public class SpellMemory : ISpellMemory
{
    public SpellMemory()
    {
        UsedSpellSlots = new int[9];
        ClassSpecificSpellMemories = [];
    }

    private Dictionary<ClassModel, ClassSpecificSpellMemory> ClassSpecificSpellMemories { get; }

    private int[] UsedSpellSlots { get; }

    public int GetUsedSpellSlots(int level)
    {
        return UsedSpellSlots[level - 1];
    }

    public void UseSpellSlot(int level)
    {
        UsedSpellSlots[level - 1]++;
    }

    public void ResetSpellSlots()
    {
        for (var i = 0; i < UsedSpellSlots.Length; i++)
        {
            UsedSpellSlots[i] = 0;
        }
    }

    public void PrepareSpell(ClassModel classModel, ISpellAction spell)
    {
        if (ClassSpecificSpellMemories.TryGetValue(classModel, out var classSpecificSpellMemory))
        {
            classSpecificSpellMemory.PrepareSpell(spell);
        }
    }

    public void UnprepareSpell(ClassModel classModel, ISpellAction spell)
    {
        if (ClassSpecificSpellMemories.TryGetValue(classModel, out var classSpecificSpellMemory))
        {
            classSpecificSpellMemory.UnprepareSpell(spell);
        }
    }

    public void LearnCantrip(ClassModel classModel, ISpellAction spell)
    {
        if (ClassSpecificSpellMemories.TryGetValue(classModel, out var classSpecificSpellMemory))
        {
            classSpecificSpellMemory.LearnCantrip(spell);
        }
    }

    public void ForgetCantrip(ClassModel classModel, ISpellAction spell)
    {
        if (ClassSpecificSpellMemories.TryGetValue(classModel, out var classSpecificSpellMemory))
        {
            classSpecificSpellMemory.ForgetCantrip(spell);
        }
    }

    public List<ISpellAction> GetCantrips()
    {
        return ClassSpecificSpellMemories.Values.SelectMany(x => x.GetCantrips()).ToList();
    }

    public List<ISpellAction> GetPreparedSpells()
    {
        return ClassSpecificSpellMemories.Values.SelectMany(x => x.GetPreparedSpells()).ToList();
    }

    public List<ISpellAction> GetCantrips(ClassModel classModel)
    {
        if (ClassSpecificSpellMemories.TryGetValue(classModel, out var classSpecificSpellMemory))
        {
            return classSpecificSpellMemory.GetCantrips();
        }

        return [];
    }

    public List<ISpellAction> GetPreparedSpells(ClassModel classModel)
    {
        if (ClassSpecificSpellMemories.TryGetValue(classModel, out var classSpecificSpellMemory))
        {
            return classSpecificSpellMemory.GetPreparedSpells();
        }

        return [];
    }

    public bool HasPreparedSpell(SpellModel spell)
    {
        return ClassSpecificSpellMemories.Values.Any(x => x.HasPreparedSpell(spell));
    }

    public bool HasCantrip(SpellModel spell)
    {
        return ClassSpecificSpellMemories.Values.Any(x => x.HasCantrip(spell));
    }

    public async Task HandleCommand(ICommand command)
    {
        foreach (var classSpecificSpellMemory in GetCantrips())
        {
            await classSpecificSpellMemory.HandleCommand(command);
        }

        foreach (var classSpecificSpellMemory in GetPreparedSpells())
        {
            await classSpecificSpellMemory.HandleCommand(command);
        }

        if (command is CanCastKnownSpell canCastKnownSpell)
        {
            if (canCastKnownSpell.Spell.Level == null || canCastKnownSpell.Spell.Level < 0 || canCastKnownSpell.Spell.Level > 9)
            {
                canCastKnownSpell.SetError("Spell level is wrong: " + canCastKnownSpell.Spell.Level);
                return;
            }

            if (canCastKnownSpell.Spell.Level == 0)
            {
                if (command.Actor.SpellMemory.HasCantrip(canCastKnownSpell.Spell))
                {
                    canCastKnownSpell.SetValue(true, $"{command.Actor.Name} can cast cantrip {canCastKnownSpell.Spell}.");
                }
                else
                {
                    canCastKnownSpell.SetValue(false, $"{command.Actor.Name} doesn't know cantrip {canCastKnownSpell.Spell}.");
                }
            }
            else
            {
                if (command.Actor.SpellMemory.HasPreparedSpell(canCastKnownSpell.Spell))
                {
                    var spellSlot = await new GetAvailableSpellSlots(command.Actor, canCastKnownSpell.Spell.Level.Value).Execute();

                    if (!spellSlot.IsSuccess)
                    {
                        canCastKnownSpell.SetError("GetAvailableSpellSlots: " + spellSlot.ErrorMessage);
                        return;
                    }

                    if (spellSlot.Value > 0)
                    {
                        canCastKnownSpell.SetValue(true, $"{command.Actor.Name} can cast spell {canCastKnownSpell.Spell}.");
                    }
                    else
                    {
                        canCastKnownSpell.SetValue(false, $"{command.Actor.Name} doesn't have spell slot for spell {canCastKnownSpell.Spell}.");
                    }
                }
                else
                {
                    canCastKnownSpell.SetValue(false, $"{command.Actor.Name} doesn't know spell {canCastKnownSpell.Spell}.");
                }
            }
        }
        else if (command is GetKnownSpells knownSpells)
        {
            foreach (var classSpecificSpellMemory in ClassSpecificSpellMemories.Values)
            {
                classSpecificSpellMemory.HandleGetKnownSpells(knownSpells);
            }
        }
        else if (command is LongRest)
        {
            ResetSpellSlots();
        }
    }
}

public class ClassSpecificSpellMemory
{
    public ClassSpecificSpellMemory(ClassModel classModel)
    {
        ClassModel = classModel;
        PreparedSpells = [];
        Cantrips = [];
    }

    public ClassModel ClassModel { get; }

    private Dictionary<SpellModel, ISpellAction> Cantrips { get; }

    private Dictionary<SpellModel, ISpellAction> PreparedSpells { get; }

    public void PrepareSpell(ISpellAction spell)
    {
        PreparedSpells.TryAdd(spell.Spell, spell);
    }

    public void UnprepareSpell(ISpellAction spell)
    {
        PreparedSpells.Remove(spell.Spell);
    }

    public void LearnCantrip(ISpellAction spell)
    {
        Cantrips.TryAdd(spell.Spell, spell);
    }

    public void ForgetCantrip(ISpellAction spell)
    {
        Cantrips.Remove(spell.Spell);
    }

    public List<ISpellAction> GetCantrips()
    {
        return Cantrips.Values.ToList();
    }

    public List<ISpellAction> GetPreparedSpells()
    {
        return PreparedSpells.Values.ToList();
    }

    public bool HasPreparedSpell(SpellModel spell)
    {
        return PreparedSpells.ContainsKey(spell);
    }

    public bool HasCantrip(SpellModel spell)
    {
        return Cantrips.ContainsKey(spell);
    }

    public void HandleGetKnownSpells(GetKnownSpells knownSpells)
    {
        knownSpells.AddValues(GetCantrips().Select(x => x.Spell), ClassModel.Name ?? "Spellcasting");
        knownSpells.AddValues(GetPreparedSpells().Select(x => x.Spell), ClassModel.Name ?? "Spellcasting");
    }
}