namespace Dnd.System.Entities.Feats;

using Dnd.System.Entities.GameActors;

public interface IFeat : IBonusProvider
{
    string Description { get; }

    bool IsEligible(IGameActor actor);
}
