namespace DnD.Entities.Characters;

using DnD.Entities.Classes;

internal class Level
{
    public Level(IDndClass dndClass, int level)
    {
        this.Class = dndClass;
        this.LevelNum = level;
    }

    public IDndClass Class { get; set; }
    public int LevelNum { get; set; }
}
