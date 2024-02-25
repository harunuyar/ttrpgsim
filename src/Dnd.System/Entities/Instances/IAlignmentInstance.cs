namespace Dnd.System.Entities.Instances;

using Dnd._5eSRD.Models.Alignment;

public interface IAlignmentInstance : ICommandHandler
{
    AlignmentModel AlignmentModel { get; }
}
