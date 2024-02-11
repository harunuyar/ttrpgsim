namespace Dnd.System.Entities.Effects;

public interface IEffect : IBonusProvider
{
    public string Description { get; }
}
