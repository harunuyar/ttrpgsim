namespace Dnd.System.Entities.Instances;

using Dnd._5eSRD.Models.Feature;
using Dnd._5eSRD.Models.Proficiency;
using Dnd.Predefined.Commands.BoolCommands;
using Dnd.System.CommandSystem.Commands;

public class FeatureInstance : IFeatureInstance
{
    public FeatureInstance(FeatureModel featureModel, IEnumerable<IFeatureInstance> subfeatureOptions, IEnumerable<ProficiencyModel> expertiseOptions, IEnumerable<IFeatureInstance> invocations)
    {
        FeatureModel = featureModel;
        SubfeatureOptions = subfeatureOptions.ToList();
        ExpertiseOptions = expertiseOptions.ToList();
        Invocations = invocations.ToList();
    }

    public FeatureModel FeatureModel { get; }

    public List<IFeatureInstance> SubfeatureOptions { get; }

    public List<ProficiencyModel> ExpertiseOptions { get; }

    public List<IFeatureInstance> Invocations { get; }

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

        if (command is HasSkillExpertise expertise)
        {
            foreach (var expertiseOption in ExpertiseOptions)
            {
                if (expertiseOption.Url == expertise.Skill.Url)
                {
                    expertise.SetValue(true, FeatureModel.Name ?? "Expertise");
                    break;
                }
            }
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
