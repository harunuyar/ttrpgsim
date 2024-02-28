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
        AffectedAreaEffects = [];
    }

    public HashSet<IAreaEffect> ActiveAreaEffects { get; }

    public HashSet<IPersonalEffect> ActivePersonalEffects { get; }

    public HashSet<IPersonalEffect> CausedPersonalEffects { get; }

    public HashSet<IAreaEffect> AffectedAreaEffects { get; }

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

    public void RemoveCausedAreaEffect(IAreaEffect effect)
    {
        ActiveAreaEffects.Remove(effect);
        foreach (var target in effect.GameActorsInArea)
        {
            target.EffectsTable.RemoveAffectedAreaEffect(effect);
        }
    }

    public void AddAffectedAreaEffect(IAreaEffect effect)
    {
        AffectedAreaEffects.Add(effect);
    }

    public void RemoveAffectedAreaEffect(IAreaEffect effect)
    {
        AffectedAreaEffects.Remove(effect);
    }

    public void RemoveCausedEffect(IEffect effect)
    {
        if (effect is IPersonalEffect personalEffect)
        {
            RemoveCausedPersonalEffect(personalEffect);
        }
        else if (effect is IAreaEffect areaEffect)
        {
            RemoveCausedAreaEffect(areaEffect);
        }
    }

    public async Task HandleCommand(ICommand command)
    {
        // ToList() to avoid concurrent modification
        foreach (var effect in ActiveAreaEffects.ToList()) 
        {
            await effect.HandleCommand(command);
        }

        foreach (var effect in AffectedAreaEffects.ToList())
        {
            await effect.HandleCommand(command);
        }

        foreach (var effect in ActivePersonalEffects.ToList())
        {
            await effect.HandleCommand(command);
        }

        foreach (var effect in CausedPersonalEffects.ToList())
        {
            await effect.HandleCommand(command);
        }
    }
}
