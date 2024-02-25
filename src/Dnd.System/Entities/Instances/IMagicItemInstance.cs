namespace Dnd.System.Entities.Instances;

using Dnd._5eSRD.Models.MagicItem;

public interface IMagicItemInstance : ICommandHandler, IUsageBonusProvider
{
    MagicItemModel MagicItemModel { get; }
}
