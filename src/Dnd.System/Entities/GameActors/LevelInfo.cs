namespace Dnd.System.Entities.GameActors;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Classes;
using Dnd.System.Entities.Levels;

public class LevelInfo
{
    public LevelInfo() 
    { 
        Levels = new Dictionary<IClass, List<ILevel>>();
    }

    private Dictionary<IClass, List<ILevel>> Levels { get; }

    public int Level => Levels.Values.Sum(list => list.Count);

    public bool AddLevel(ILevel newLevel)
    {
        if (!Levels.TryGetValue(newLevel.Class, out var currentLevels))
        {
            currentLevels = new List<ILevel>();
            Levels.Add(newLevel.Class, currentLevels);
        }

        int lastLevel = currentLevels.Count > 0 ? currentLevels.Last().Level : 0;

        if (newLevel.Level != lastLevel + 1)
        {
            return false;
        }

        currentLevels.Add(newLevel);
        return true;
    }
    
    public int GetLevelsInClass(IClass dndClass)
    {
        return Levels.TryGetValue(dndClass, out var levels) ? levels.Count : 0;
    }

    public List<ILevel> GetLevels()
    {
        return Levels.Select(pair => pair.Value.Last()).ToList();
    }

    public ILevel? GetLevelForClass(IClass dndClass)
    {
        return Levels.GetValueOrDefault(dndClass)?.Last();
    }

    public void HandleCommand(ICommand command)
    {
        foreach (var feat in Levels.Values.SelectMany(levelList => levelList).SelectMany(level => level.Feats))
        {
            feat.HandleCommand(command);
        }
    }
}
