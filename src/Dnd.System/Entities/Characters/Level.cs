namespace Dnd.System.Entities.Characters;

using Dnd.System.Entities.Classes;

public class Level
{
    public Level(IClass dndClass, int level)
    {
        this.Class = dndClass;
        this.LevelNum = level;
    }

    public IClass Class { get; set; }

    public int LevelNum { get; set; }
}
