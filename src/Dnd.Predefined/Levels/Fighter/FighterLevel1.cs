﻿namespace Dnd.Predefined.Levels.Fighter;

using Dnd.Predefined.Classes;
using Dnd.Predefined.Feats.Classes.Fighter;
using Dnd.Predefined.Feats.Classes.Fighter.FightingStyle;
using Dnd.System.Entities.Skills;

public class FighterLevel1 : SharedLevel1
{
    // For multiclassing
    public FighterLevel1(Fighter fighterClass, IFightingStyle fightingStyle) : base(fighterClass, "Fighter Level 1", true, []) 
    {
        Feats.Add(fightingStyle);
        Feats.Add(new SecondWind());
    }

    public FighterLevel1(Fighter fighterClass, ISkill skill1, ISkill skill2, IFightingStyle fightingStyle) : base(fighterClass, "Fighter Level 1", false, [skill1, skill2]) 
    {
        Feats.Add(fightingStyle);
        Feats.Add(new SecondWind());
    }
}