namespace Dnd.System.Entities.GameActor;

using Dnd._5eSRD.Models.Class;
using Dnd.System.Entities.Instances;

public interface ILevelInfo
{
    IClassInstance MainClass { get; }
    List<IClassInstance> MultiClasses { get; }
    int Level { get; }
    bool AddLevel(ILevelInstance newLevel);
    int GetLevelsInClass(ClassModel dndClass);
    List<ILevelInstance> GetLevels();
    List<ILevelInstance> GetLastLevels();
    ILevelInstance? GetLevelForClass(ClassModel dndClass);
    List<IClassInstance> GetClasses();
    ISubclassInstance? GetSubclassForClass(ClassModel dndClass);
}
