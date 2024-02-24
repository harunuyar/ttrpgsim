namespace Dnd.System.Entities.Instances;

using Dnd._5eSRD.Models.MagicItem;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities;

public class MagicItemInstance : IUsageBonusProvider
{
    public MagicItemInstance(MagicItemModel magicItemModel)
    {
        MagicItemModel = magicItemModel;
    }

    public MagicItemModel MagicItemModel { get; }

    public virtual Task HandleCommand(ICommand command)
    {
        return Task.CompletedTask;
    }

    public virtual Task HandleUsageCommand(ICommand command)
    {
        return Task.CompletedTask;
    }

    public override bool Equals(object? obj)
    {
        return obj is MagicItemInstance magicItemInstance
            && magicItemInstance.MagicItemModel == MagicItemModel;
    }

    public override int GetHashCode()
    {
        return MagicItemModel.GetHashCode();
    }
}
