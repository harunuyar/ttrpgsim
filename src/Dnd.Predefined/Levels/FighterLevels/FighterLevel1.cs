namespace Dnd.Predefined.Levels.FighterLevels;

using Dnd.Predefined.Classes;
using Dnd.Predefined.Feats.Classes.Fighter.Level1;
using Dnd.Predefined.Feats.Classes.Fighter.Level1.FightingStyle;
using Dnd.System.Entities.Skills;

public class FighterLevel1 : SharedLevel1
{
    public FighterLevel1(IFightingStyle fightingStyle) : base(Fighter.Instance, "Fighter Level 1", true, []) 
    {
        Feats.Add(fightingStyle);
        Feats.Add(new SecondWind());
    }

    public FighterLevel1(ISkill skill1, ISkill skill2, IFightingStyle fightingStyle) : base(Fighter.Instance, "Fighter Level 1", false, [skill1, skill2]) 
    {
        Feats.Add(fightingStyle);
        Feats.Add(new SecondWind());
    }
}
