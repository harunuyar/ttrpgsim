namespace Dnd.Predefined.GameActor;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Effect;
using Dnd.System.Entities.GameActor;

public class EffectsTable : IEffectsTable
{
    public EffectsTable()
    {
        ActivePersonalEffects = [];
        CausedPersonalEffects = [];
        ActiveAreaEffects = [];
    }

    public HashSet<IAreaEffect> ActiveAreaEffects { get; }

    public HashSet<IPersonalEffect> ActivePersonalEffects { get; }

    public HashSet<IPersonalEffect> CausedPersonalEffects { get; }

    public void AddCausedPersonalEffect(IPersonalEffect effect)
    {
        effect.Target.EffectsTable.ActivePersonalEffects.Add(effect);
        CausedPersonalEffects.Add(effect);
    }

    public void RemoveCausedPersonalEffect(IPersonalEffect effect)
    {
        effect.Target.EffectsTable.ActivePersonalEffects.Remove(effect);
        CausedPersonalEffects.Remove(effect);
    }

    public void AddActiveAreaEffect(IAreaEffect effect)
    {
        ActiveAreaEffects.Add(effect);
    }

    public void RemoveActiveAreaEffect(IAreaEffect effect)
    {
        ActiveAreaEffects.Remove(effect);
    }

    public async Task HandleCommand(ICommand command)
    {
        foreach (var effect in ActiveAreaEffects)
        {
            await effect.ActivateEffect();
            await effect.HandleCommand(command);

            if (effect.IsExpired)
            {
                RemoveActiveAreaEffect(effect);
            }
        }

        foreach (var effect in ActivePersonalEffects)
        {
            await effect.ActivateEffect();
            await effect.HandleCommand(command);

            if (effect.IsExpired)
            {
                effect.Source.EffectsTable.RemoveCausedPersonalEffect(effect);
            }
        }

        foreach (var effect in CausedPersonalEffects)
        {
            await effect.HandleCommand(command);

            if (effect.IsExpired)
            {
                RemoveCausedPersonalEffect(effect);
            }
        }
    }
}
