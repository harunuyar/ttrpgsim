namespace Dnd.System.Entities.Instances;

using Dnd._5eSRD.Models.Level;
using Dnd.System.CommandSystem.Commands;

public class LevelInstance : ILevelInstance
{
    public LevelInstance(LevelModel levelModel, IClassInstance classInstance, IEnumerable<IFeatureInstance> features)
    {
        LevelModel = levelModel;
        ClassInstance = classInstance;
        Features = features.ToList();
    }

    public LevelModel LevelModel { get; }

    public IClassInstance ClassInstance { get; }

    public List<IFeatureInstance> Features { get; }

    public override bool Equals(object? obj)
    {
        return obj is LevelInstance levelInstance
            && levelInstance.LevelModel == LevelModel
            && levelInstance.ClassInstance.Equals(ClassInstance)
            && levelInstance.Features.Equals(Features);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(LevelModel, ClassInstance, Features);
    }

    public async Task HandleCommand(ICommand command)
    {
        foreach (var feature in Features)
        {
            await feature.HandleCommand(command);
        }
    }
}
