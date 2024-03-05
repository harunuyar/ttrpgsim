namespace Dnd.Predefined.Definitions.Levels.Fighter;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.Level;
using Dnd.Context;
using Dnd.Predefined.Definitions.Classes;
using Dnd.Predefined.Definitions.Features.Fighter;
using Dnd.Predefined.Instances;
using Dnd.System.Entities.Instances;

public class FighterLevel1 : LevelInstance
{
    public static async Task<FighterLevel1> Create(Fighter fighter, ISubFightingStyle subFightingStyle)
    {
        var fightingStyle = await FightingStyle.Create(subFightingStyle);
        var levelModel = await DndContext.Instance.GetObject<LevelModel>(Levels.Fighter1);
        return levelModel == null 
            ? throw new InvalidOperationException("Fighter level 1 model is not found") 
            : new FighterLevel1(levelModel, fighter, [fightingStyle, await SecondWind.Create()]);
    }

    public FighterLevel1(LevelModel levelModel, Fighter fighter, IEnumerable<IFeatureInstance> features) 
        : base(levelModel, fighter, null, features)
    {
    }
}
