using TableTopRpg.Entities.Character;

namespace TableTopRpg.Entities;

public interface IGameActor
{
    string Name { get; set; }
    int Level { get; }
    IRace Race { get; set; }
    IAlignment Alignment { get; set; }
    List<ITrait> Traits { get; }
    List<IFeat> Feats { get; }
}
