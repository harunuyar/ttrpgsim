namespace Dnd.System.Entities.GameActors;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Classes;
using Dnd.System.Entities.Levels;

public class LevelInfo
{
    public LevelInfo() 
    {
        Levels = new List<ILevel>();
    }

    private List<ILevel> Levels { get; }

    public int Level => Levels.Count;

    public bool AddLevel(ILevel newLevel)
    {
        var currentLevel = Levels.Where(l => l.Class == newLevel.Class).Select(l => l.Level).Max();
        
        if (newLevel.Level != currentLevel + 1)
        {
            return false;
        }

        Levels.Add(newLevel);
        return true;
    }

    public List<ILevel> GetLevels()
    {
        return Levels
            .GroupBy(l => l.Class)
            .Select(group => group.OrderByDescending(l => l.Level).First())
            .ToList();
    }
    
    public int GetLevelsInClass(IClass dndClass)
    {
        return Levels.Where(l => l.Class == dndClass).Count();
    }

    public ILevel? GetLevelForClass(IClass dndClass)
    {
        return Levels.Where(l => l.Class == dndClass).MaxBy(x => x.Level);
    }

    public void HandleCommand(ICommand command)
    {
        foreach (var feat in Levels.SelectMany(level => level.Feats))
        {
            feat.HandleCommand(command);
        }
    }
}
