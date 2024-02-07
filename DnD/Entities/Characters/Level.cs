namespace DnD.Entities.Characters;

using DnD.Entities.Classes;

internal class Level
{
    public Level(IClass dndClass, int level)
    {
        this.Class = dndClass;
        this.LevelNum = level;
    }

    public IClass Class { get; set; }
    public int LevelNum { get; set; }
}
