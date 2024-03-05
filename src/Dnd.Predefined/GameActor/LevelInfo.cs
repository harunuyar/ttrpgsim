namespace Dnd.Predefined.GameActor;

using Dnd._5eSRD.Models.Class;
using Dnd.System.Entities.GameActor;
using Dnd.System.Entities.Instances;

public class LevelInfo : ILevelInfo
{
    public LevelInfo(ILevelInstance firstLevel)
    {
        Levels = [];
        MultiClasses = [];
        MainClass = firstLevel.ClassInstance;
        Levels.Add(firstLevel.ClassInstance.ClassModel, [firstLevel]);
    }

    public IClassInstance MainClass { get; private set; }

    public List<IClassInstance> MultiClasses { get; }

    private Dictionary<ClassModel, List<ILevelInstance>> Levels { get; }

    public int Level => Levels.Values.Sum(list => list.Count);

    public bool AddLevel(ILevelInstance newLevel)
    {
        if (!Levels.TryGetValue(newLevel.ClassInstance.ClassModel, out var currentLevels))
        {
            currentLevels = new List<ILevelInstance>();
            Levels.Add(newLevel.ClassInstance.ClassModel, currentLevels);

            MultiClasses.Add(newLevel.ClassInstance);
        }

        int lastLevel = currentLevels.Count > 0 ? currentLevels.Last().LevelModel.LevelNumber ?? 0 : 0;

        if (newLevel.LevelModel.LevelNumber != lastLevel + 1)
        {
            return false;
        }

        currentLevels.Add(newLevel);
        return true;
    }

    public int GetLevelsInClass(ClassModel dndClass)
    {
        return Levels.TryGetValue(dndClass, out var levels) ? levels.Count : 0;
    }

    public ILevelInstance? GetLevelForClass(ClassModel dndClass)
    {
        return Levels.GetValueOrDefault(dndClass)?.Last();
    }

    public List<IClassInstance> GetClasses()
    {
        return MainClass == null ? MultiClasses.ToList() : MultiClasses.Prepend(MainClass).ToList();
    }

    public List<ILevelInstance> GetLastLevels()
    {
        return Levels.Select(pair => pair.Value.Last()).ToList();
    }

    public List<ILevelInstance> GetLevels()
    {
        return Levels.Values.SelectMany(list => list).ToList();
    }

    public ISubclassInstance? GetSubclassForClass(ClassModel dndClass)
    {
        return GetLevelForClass(dndClass)?.SubclassInstance;
    }
}
