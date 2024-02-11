namespace Dnd.System.Entities.Feats;

public interface IFeat : IBonusProvider
{
    string Description { get; }
}
