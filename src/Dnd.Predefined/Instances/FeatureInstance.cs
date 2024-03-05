namespace Dnd.Predefined.Instances;

using Dnd._5eSRD.Models.Feature;
using Dnd._5eSRD.Models.Proficiency;
using Dnd.Predefined.Commands.BoolCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Instances;

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

    public virtual Task HandleCommand(ICommand command)
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
}
