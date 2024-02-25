namespace Dnd.System.Entities.Instances;

using Dnd._5eSRD.Models.Feature;
using Dnd._5eSRD.Models.Proficiency;

public interface IFeatureInstance : ICommandHandler
{
    FeatureModel FeatureModel { get; }
    List<IFeatureInstance> SubfeatureOptions { get; }
    List<ProficiencyModel> ExpertiseOptions { get; }
    List<IFeatureInstance> Invocations { get; }
}
