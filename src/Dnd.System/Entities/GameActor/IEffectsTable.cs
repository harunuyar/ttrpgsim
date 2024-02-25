namespace Dnd.System.Entities.GameActor;

using Dnd.System.Entities.Effect;

public interface IEffectsTable : ICommandHandler
{
    public HashSet<IAreaEffect> ActiveAreaEffects { get; }
    public HashSet<IPersonalEffect> ActivePersonalEffects { get; }
    public HashSet<IPersonalEffect> CausedPersonalEffects { get; }
    public void AddCausedPersonalEffect(IPersonalEffect effect);
    public void RemoveCausedPersonalEffect(IPersonalEffect effect);
    public void AddActiveAreaEffect(IAreaEffect effect);
    public void RemoveActiveAreaEffect(IAreaEffect effect);
}
