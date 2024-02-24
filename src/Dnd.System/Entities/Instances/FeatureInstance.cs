namespace Dnd.System.Entities.Instances;

using Dnd._5eSRD.Models.Feature;
using Dnd._5eSRD.Models.Proficiency;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities;

public class FeatureInstance : IBonusProvider
{
    public FeatureInstance(FeatureModel featureModel, IEnumerable<FeatureInstance> subfeatureOptions, IEnumerable<ProficiencyModel> expertiseOptions, IEnumerable<FeatureInstance> invocations)
    {
        FeatureModel = featureModel;
        SubfeatureOptions = subfeatureOptions.ToList();
        ExpertiseOptions = expertiseOptions.ToList();
        Invocations = invocations.ToList();
    }

    public FeatureModel FeatureModel { get; }

    public List<FeatureInstance> SubfeatureOptions { get; }

    public List<ProficiencyModel> ExpertiseOptions { get; }

    public List<FeatureInstance> Invocations { get; }

    public Task HandleCommand(ICommand command)
    {
        foreach (var subfeature in SubfeatureOptions)
        {
            subfeature.HandleCommand(command);
        }

        foreach (var invocation in Invocations)
        {
            invocation.HandleCommand(command);
        }

        return Task.CompletedTask;
    }

    public override bool Equals(object? obj)
    {
        return obj is FeatureInstance featureInstance
            && featureInstance.FeatureModel == FeatureModel
            && featureInstance.SubfeatureOptions.Equals(SubfeatureOptions)
            && featureInstance.ExpertiseOptions.Equals(ExpertiseOptions)
            && featureInstance.Invocations.Equals(Invocations);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(FeatureModel, SubfeatureOptions, ExpertiseOptions, Invocations);
    }
}
