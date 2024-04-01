namespace Dnd.Predefined.Instances;

using Dnd._5eSRD.Models.Alignment;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Instances;

public class AlignmentInstance : IAlignmentInstance
{
    public AlignmentInstance(AlignmentModel alignmentModel)
    {
        AlignmentModel = alignmentModel;
    }

    public AlignmentModel AlignmentModel { get; }

    public Task HandleCommand(ICommand command)
    {
        return Task.CompletedTask;
    }
}
