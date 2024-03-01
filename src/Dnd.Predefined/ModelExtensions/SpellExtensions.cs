namespace Dnd.Predefined.ModelExtensions;

using Dnd._5eSRD.Models.Spell;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.Effect;
using Dnd.System.GameManagers.Dice;

public static class SpellExtensions
{
    public static EffectDuration? GetSpellEffectDuration(this SpellModel spellModel)
    {
        return EffectDuration.FromString(spellModel.Duration);
    }

    public static ActionRange? GetSpellRange(this SpellModel spellModel)
    {
        return ActionRange.FromString(spellModel.Range);
    }

    public static ActionDurationType? GetSpellActionDuration(this SpellModel spellModel)
    {
        return ActionDurationType.FromString(spellModel.CastingTime);
    }

    public static DicePool GetDamage(this SpellModel spellModel, int charLevel, int spellLevel)
    {
        if (spellModel.Damage?.DamageAtSlotLevel?.TryGetValue(spellLevel, out var damage) ?? false)
        {
            return DicePool.Parse(damage);
        }

        if (spellModel.Damage?.DamageAtCharacterLevel?.TryGetValue(charLevel, out damage) ?? false)
        {
            return DicePool.Parse(damage);
        }

        return new DicePool([], 0);
    }

    public static DicePool GetDamageAtSlotLevel(this SpellModel spellModel, int spellLevel)
    {
        if (spellModel.Damage?.DamageAtSlotLevel?.TryGetValue(spellLevel, out var damage) ?? false)
        {
            return DicePool.Parse(damage);
        }

        return new DicePool([], 0);
    }

    public static DicePool GetDamageAtCharacterLevel(this SpellModel spellModel, int charLevel)
    {
        if (spellModel.Damage?.DamageAtCharacterLevel?.TryGetValue(charLevel, out var damage) ?? false)
        {
            return DicePool.Parse(damage);
        }

        return new DicePool([], 0);
    }

    public static Dictionary<int, DicePool> GetHealAtSlotLevel(this SpellModel spellModel)
    {
        var healAtSlotLevel = new Dictionary<int, DicePool>();

        foreach (var pair in spellModel.HealAtSlotLevel ?? [])
        {
            healAtSlotLevel.Add(pair.Key, DicePool.Parse(pair.Value));
        }

        return healAtSlotLevel;
    }
}
