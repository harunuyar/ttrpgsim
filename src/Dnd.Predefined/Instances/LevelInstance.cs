namespace Dnd.Predefined.Instances;

using Dnd._5eSRD.Models.Level;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Instances;

public class LevelInstance : ILevelInstance
{
    public LevelInstance(LevelModel levelModel, IClassInstance classInstance, ISubclassInstance? subclassInstance, IEnumerable<IFeatureInstance> features)
    {
        LevelModel = levelModel;
        ClassInstance = classInstance;
        SubclassInstance = subclassInstance;
        Features = features.ToList();
    }

    public LevelModel LevelModel { get; }

    public IClassInstance ClassInstance { get; }

    public List<IFeatureInstance> Features { get; }

    public ISubclassInstance? SubclassInstance { get; }

    public async Task HandleCommand(ICommand command)
    {
        foreach (var feature in Features)
        {
            await feature.HandleCommand(command);
        }
    }
}
