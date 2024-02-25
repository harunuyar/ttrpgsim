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

    public static Dictionary<int, DicePool> GetDamageAtSlotLevel(this SpellModel spellModel)
    {
        var damageAtSlotLevel = new Dictionary<int, DicePool>();

        foreach (var pair in spellModel.Damage?.DamageAtSlotLevel ?? [])
        {
            damageAtSlotLevel.Add(pair.Key, DicePool.Parse(pair.Value));
        }

        return damageAtSlotLevel;
    }

    public static Dictionary<int, DicePool> GetDamageAtCharacterLevel(this SpellModel spellModel)
    {
        var damageAtCharLevel = new Dictionary<int, DicePool>();

        foreach (var pair in spellModel.Damage?.DamageAtCharacterLevel ?? [])
        {
            damageAtCharLevel.Add(pair.Key, DicePool.Parse(pair.Value));
        }

        return damageAtCharLevel;
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
