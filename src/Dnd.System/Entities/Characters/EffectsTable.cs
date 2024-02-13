namespace Dnd.System.Entities.Characters;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.CommandSystem.Commands.EventCommands;
using Dnd.System.CommandSystem.Commands.RollCommands;
using Dnd.System.Entities.Effects;
using Dnd.System.Entities.Spells;

public class EffectsTable
{
    public EffectsTable()
    {
        ActiveEffects = new HashSet<IEffect>();
        CausedEffects = new HashSet<IEffect>();
    }

    public HashSet<IEffect> ActiveEffects { get; private set; }

    public HashSet<IEffect> CausedEffects { get; private set; }

    public (ISpell Spell, IEffect Effect)? Concentration { get; private set; }

    public void AddCausedEffect(IEffect effect)
    {
        effect.Target.EffectsTable.ActiveEffects.Add(effect);
        CausedEffects.Add(effect);
    }

    public void RemoveCausedEffect(IEffect effect)
    {
        effect.Target.EffectsTable.ActiveEffects.Remove(effect);
        CausedEffects.Remove(effect);

        if (Concentration?.Effect == effect)
        {
            Concentration = null;
        }
    }

    public void AddConcentration(ISpell spell, IEffect effect)
    {
        Concentration = (spell, effect);
        AddCausedEffect(effect);
    }

    public void RemoveConcentration()
    {
        if (Concentration != null)
        {
            RemoveCausedEffect(Concentration.Value.Effect);
        }
    }

    public void HandleCommand(ICommand command)
    {
        foreach (var effect in ActiveEffects)
        {
            effect.HandleCommand(command);
        }

        if (Concentration != null && command is ApplyDamage applyDamage)
        {
            var rollConcentrationSavingThrow = new RollConcentrationSavingThrow(applyDamage.EventListener, applyDamage.Character, applyDamage.Damage, applyDamage.DamageType);
            _ = rollConcentrationSavingThrow.Execute();
        }

        if (command is TakeTurn || command is PassTime)
        {
            foreach (var effect in CausedEffects)
            {
                effect.Duration.HandleCommand(command);
                if (effect.Duration.IsExpired())
                {
                    RemoveCausedEffect(effect);
                }
            }
        }
    }
}
