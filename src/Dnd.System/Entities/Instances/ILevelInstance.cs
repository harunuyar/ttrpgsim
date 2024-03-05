namespace Dnd.System.Entities.Instances;

using Dnd._5eSRD.Models.Level;

public interface ILevelInstance : ICommandHandler
{
    public LevelModel LevelModel { get; }
    public IClassInstance ClassInstance { get; }
    public ISubclassInstance? SubclassInstance { get; }
    public List<IFeatureInstance> Features { get; }
}
