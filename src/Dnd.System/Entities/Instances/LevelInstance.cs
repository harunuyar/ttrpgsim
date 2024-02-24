namespace Dnd.System.Entities.Instances;

using Dnd._5eSRD.Models.Level;

public class LevelInstance
{
    public LevelInstance(LevelModel levelModel, ClassInstance classInstance, IEnumerable<FeatureInstance> features)
    {
        LevelModel = levelModel;
        ClassInstance = classInstance;
        Features = features.ToList();
    }

    public LevelModel LevelModel { get; }

    public ClassInstance ClassInstance { get; }

    public List<FeatureInstance> Features { get; }

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
}
