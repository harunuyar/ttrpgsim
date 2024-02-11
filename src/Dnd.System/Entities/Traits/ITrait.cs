namespace Dnd.System.Entities.Traits;

public interface ITrait : IBonusProvider
{
    public string Description { get; }
}
