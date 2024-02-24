namespace Dnd.System.Entities.GameActor;

using Dnd._5eSRD.Models.Class;
using Dnd.System.Entities.Instances;

public class LevelInfo
{
    public LevelInfo()
    {
        Levels = [];
        MultiClasses = [];
    }

    public ClassInstance? MainClass { get; private set; }

    public List<ClassInstance> MultiClasses { get; }

    private Dictionary<ClassModel, List<LevelInstance>> Levels { get; }

    public int Level => Levels.Values.Sum(list => list.Count);

    public bool AddLevel(LevelInstance newLevel)
    {
        if (!Levels.TryGetValue(newLevel.ClassInstance.ClassModel, out var currentLevels))
        {
            currentLevels = new List<LevelInstance>();
            Levels.Add(newLevel.ClassInstance.ClassModel, currentLevels);

            if (MainClass == null)
            {
                MainClass = newLevel.ClassInstance;
            }
            else
            {
                MultiClasses.Add(newLevel.ClassInstance);
            }
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

    public List<LevelInstance> GetLevels()
    {
        return Levels.Select(pair => pair.Value.Last()).ToList();
    }

    public LevelInstance? GetLevelForClass(ClassModel dndClass)
    {
        return Levels.GetValueOrDefault(dndClass)?.Last();
    }

    public List<ClassInstance> GetClasses()
    {
        return MainClass == null ? MultiClasses.ToList() : MultiClasses.Prepend(MainClass).ToList();
    }
}
