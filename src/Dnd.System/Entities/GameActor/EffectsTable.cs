namespace Dnd.System.Entities.GameActor;

using Dnd.System.Entities.Effect;

public class EffectsTable
{
    public EffectsTable()
    {
        ActiveEffects = [];
        CausedEffects = [];
    }

    public HashSet<IEffect> ActiveEffects { get; private set; }

    public HashSet<IEffect> CausedEffects { get; private set; }

    public void AddCausedEffect(IEffect effect)
    {
        effect.Target.EffectsTable.ActiveEffects.Add(effect);
        CausedEffects.Add(effect);
    }

    public void RemoveCausedEffect(IEffect effect)
    {
        effect.Target.EffectsTable.ActiveEffects.Remove(effect);
        CausedEffects.Remove(effect);
    }
}
