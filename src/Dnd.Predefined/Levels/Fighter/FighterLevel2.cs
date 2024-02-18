namespace Dnd.Predefined.Levels.Fighter;

using Dnd.Predefined.Classes;
using Dnd.Predefined.Feats.Classes.Fighter;
using Dnd.System.Entities.Classes;
using Dnd.System.Entities.Feats;
using Dnd.System.Entities.Levels;

public class FighterLevel2 : ILevel
{
    public FighterLevel2(Fighter fighterClass)
    {
        Class = fighterClass;
        Feats = [new ActionSurgeFeat(1)];
    }

    public string Name => "Fighter Level 2";

    public int Level => 2;

    public IClass Class { get; }

    public List<IFeat> Feats { get; }

    public string? Subclass => null;
}
