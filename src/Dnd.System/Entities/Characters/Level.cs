namespace Dnd.Entities.Characters;

using Dnd.Entities.Classes;

public class Level
{
    public Level(IDndClass dndClass, int level)
    {
        this.Class = dndClass;
        this.LevelNum = level;
    }

    public IDndClass Class { get; set; }

    public int LevelNum { get; set; }
}
