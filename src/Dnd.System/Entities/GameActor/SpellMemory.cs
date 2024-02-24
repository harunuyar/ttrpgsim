namespace Dnd.System.Entities.GameActor;

using Dnd._5eSRD.Models.Class;
using Dnd._5eSRD.Models.Spell;

public class SpellMemory
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

    public void PrepareSpell(ClassModel classModel, SpellModel spell)
    {
        if (ClassSpecificSpellMemories.TryGetValue(classModel, out var classSpecificSpellMemory))
        {
            classSpecificSpellMemory.PrepareSpell(spell);
        }
    }

    public void UnprepareSpell(ClassModel classModel, SpellModel spell)
    {
        if (ClassSpecificSpellMemories.TryGetValue(classModel, out var classSpecificSpellMemory))
        {
            classSpecificSpellMemory.UnprepareSpell(spell);
        }
    }

    public void LearnCantrip(ClassModel classModel, SpellModel spell)
    {
        if (ClassSpecificSpellMemories.TryGetValue(classModel, out var classSpecificSpellMemory))
        {
            classSpecificSpellMemory.LearnCantrip(spell);
        }
    }

    public void ForgetCantrip(ClassModel classModel, SpellModel spell)
    {
        if (ClassSpecificSpellMemories.TryGetValue(classModel, out var classSpecificSpellMemory))
        {
            classSpecificSpellMemory.ForgetCantrip(spell);
        }
    }

    public List<SpellModel> GetCantrips()
    {
        return ClassSpecificSpellMemories.Values.SelectMany(x => x.GetCantrips()).ToList();
    }

    public List<SpellModel> GetPreparedSpells()
    {
        return ClassSpecificSpellMemories.Values.SelectMany(x => x.GetPreparedSpells()).ToList();
    }

    public List<SpellModel> GetCantrips(ClassModel classModel)
    {
        if (ClassSpecificSpellMemories.TryGetValue(classModel, out var classSpecificSpellMemory))
        {
            return classSpecificSpellMemory.GetCantrips();
        }

        return [];
    }

    public List<SpellModel> GetPreparedSpells(ClassModel classModel)
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

    private HashSet<SpellModel> Cantrips { get; }

    private HashSet<SpellModel> PreparedSpells { get; }

    public void PrepareSpell(SpellModel spell)
    {
        PreparedSpells.Add(spell);
    }

    public void UnprepareSpell(SpellModel spell)
    {
        PreparedSpells.Remove(spell);
    }

    public void LearnCantrip(SpellModel spell)
    {
        Cantrips.Add(spell);
    }

    public void ForgetCantrip(SpellModel spell)
    {
        Cantrips.Remove(spell);
    }

    public List<SpellModel> GetCantrips()
    {
        return Cantrips.ToList();
    }

    public List<SpellModel> GetPreparedSpells()
    {
        return PreparedSpells.ToList();
    }

    public bool HasPreparedSpell(SpellModel spell)
    {
        return PreparedSpells.Contains(spell);
    }

    public bool HasCantrip(SpellModel spell)
    {
        return Cantrips.Contains(spell);
    }
}