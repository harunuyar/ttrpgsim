namespace Dnd.System.Entities.Levels;

using Dnd.System.Entities.Classes;
using Dnd.System.Entities.Feats;

public interface ILevel : IDndEntity
{
    int Level { get; }

    IClass Class { get; }

    List<IFeat> Feats { get; }
}
