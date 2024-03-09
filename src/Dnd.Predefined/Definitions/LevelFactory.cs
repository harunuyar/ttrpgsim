namespace Dnd.Predefined.Definitions;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.Level;
using Dnd._5eSRD.Models.Subclass;
using Dnd.Context;
using Dnd.Predefined.Definitions.Features.Fighter;
using Dnd.Predefined.Definitions.Features.Fighter.Champion;
using Dnd.Predefined.Instances;
using Dnd.System.Entities.Instances;

public static class LevelFactory
{
    public static async Task<ILevelInstance> CreateLevelInstance(string levelUrl, string? subclassLevelUrl, IClassInstance classInstance, ISubclassInstance? subclassInstance, IEnumerable<IFeatureInstance> features)
    {
        var levelModel = await DndContext.Instance.GetObject<LevelModel>(levelUrl);
        var subclassLevelModel = subclassLevelUrl == null ? null : await DndContext.Instance.GetObject<LevelModel>(subclassLevelUrl);
        return levelModel == null 
            ? throw new InvalidOperationException($"Level model for {levelUrl} is not found") 
            : new LevelInstance(levelModel, subclassLevelModel, classInstance, subclassInstance, features);
    }

    public static async Task<ILevelInstance> FighterLevel1(IClassInstance fighterClass, ISubFightingStyle subFightingStyle)
    {
        if (fighterClass.ClassModel.Url != _5eSRD.Constants.Classes.Fighter)
        {
            throw new InvalidOperationException("Fighter level 1 can only be created from fighter class");
        }

        return await CreateLevelInstance(Levels.Fighter1, null, fighterClass, null, [await FightingStyle.Create(subFightingStyle), await SecondWind.Create()]);
    }

    public static async Task<ILevelInstance> FighterLevel2(ILevelInstance fighterLevel1)
    {
        if (fighterLevel1.LevelModel.Url != Levels.Fighter1)
        {
            throw new InvalidOperationException("Fighter level 2 can only be created from fighter level 1");
        }

        return await CreateLevelInstance(Levels.Fighter2, null, fighterLevel1.ClassInstance, fighterLevel1.SubclassInstance, [await ActionSurge.Create()]);
    }

    public static async Task<ILevelInstance> FighterLevel3Champion(ILevelInstance fighterLevel2)
    {
        if (fighterLevel2.LevelModel.Url != Levels.Fighter2)
        {
            throw new InvalidOperationException("Champion level 3 can only be created from fighter level 2");
        }

        var subclassModel = await DndContext.Instance.GetObject<SubclassModel>(Subclasses.Champion);
        if (subclassModel == null)
        {
            throw new InvalidOperationException($"Subclass model for {Subclasses.Champion} is not found");
        }

        var subclass = new SubclassInstance(subclassModel, null);

        return await CreateLevelInstance(Levels.Fighter3, SubclassLevels.Champion3, fighterLevel2.ClassInstance, subclass, [await ImprovedCritical.Create()]);
    }

    public static async Task<ILevelInstance> FighterLevel4(ILevelInstance fighterLevel3, IFeatureInstance chosenFeature)
    {
        if (fighterLevel3.LevelModel.Url != Levels.Fighter3)
        {
            throw new InvalidOperationException("Fighter level 4 can only be created from fighter level 3");
        }

        return await CreateLevelInstance(Levels.Fighter4, null, fighterLevel3.ClassInstance, fighterLevel3.SubclassInstance, [chosenFeature]);
    }

    public static async Task<ILevelInstance> FighterLevel5(ILevelInstance fighterLevel4)
    {
        if (fighterLevel4.LevelModel.Url != Levels.Fighter4)
        {
            throw new InvalidOperationException("Fighter level 5 can only be created from fighter level 4");
        }

        return await CreateLevelInstance(Levels.Fighter5, null, fighterLevel4.ClassInstance, fighterLevel4.SubclassInstance, [await ExtraAttack.Create()]);
    }

    public static async Task<ILevelInstance> FighterLevel6(ILevelInstance fighterLevel5, IFeatureInstance chosenFeature)
    {
        if (fighterLevel5.LevelModel.Url != Levels.Fighter5)
        {
            throw new InvalidOperationException("Fighter level 6 can only be created from fighter level 5");
        }

        return await CreateLevelInstance(Levels.Fighter6, null, fighterLevel5.ClassInstance, fighterLevel5.SubclassInstance, [chosenFeature]);
    }

    public static async Task<ILevelInstance> FighterLevel7Champion(ILevelInstance fighterLevel6)
    {
        if (fighterLevel6.LevelModel.Url != Levels.Fighter6)
        {
            throw new InvalidOperationException("Fighter level 7 can only be created from fighter level 6");
        }

        if (fighterLevel6.SubclassInstance?.SubclassModel.Url != Subclasses.Champion)
        {
            throw new InvalidOperationException("Champion level 7 can only be created from champion level 6");
        }

        return await CreateLevelInstance(Levels.Fighter7, SubclassLevels.Champion7, fighterLevel6.ClassInstance, fighterLevel6.SubclassInstance, [await RemarkableAthlete.Create()]);
    }

    public static async Task<ILevelInstance> FighterLevel8(ILevelInstance fighterLevel7, IFeatureInstance chosenFeature)
    {
        if (fighterLevel7.LevelModel.Url != Levels.Fighter7)
        {
            throw new InvalidOperationException("Fighter level 8 can only be created from fighter level 7");
        }

        return await CreateLevelInstance(Levels.Fighter8, null, fighterLevel7.ClassInstance, fighterLevel7.SubclassInstance, [chosenFeature]);
    }

    public static async Task<ILevelInstance> FighterLevel9(ILevelInstance fighterLevel8)
    {
        if (fighterLevel8.LevelModel.Url != Levels.Fighter8)
        {
            throw new InvalidOperationException("Fighter level 9 can only be created from fighter level 8");
        }

        return await CreateLevelInstance(Levels.Fighter9, null, fighterLevel8.ClassInstance, fighterLevel8.SubclassInstance, [await Indomitable.Create()]);
    }

    public static async Task<ILevelInstance> FighterLevel10Champion(ILevelInstance fighterLevel9, ISubFightingStyle subFightingStyle)
    {
        if (fighterLevel9.LevelModel.Url != Levels.Fighter9)
        {
            throw new InvalidOperationException("Fighter level 10 can only be created from fighter level 9");
        }

        if (fighterLevel9.SubclassInstance?.SubclassModel.Url != Subclasses.Champion)
        {
            throw new InvalidOperationException("Champion level 10 can only be created from champion level 9");
        }

        return await CreateLevelInstance(Levels.Fighter10, SubclassLevels.Champion10, fighterLevel9.ClassInstance, fighterLevel9.SubclassInstance, [await FightingStyle.Create(subFightingStyle)]);
    }

    public static async Task<ILevelInstance> FighterLevel11(ILevelInstance fighterLevel10)
    {
        if (fighterLevel10.LevelModel.Url != Levels.Fighter10)
        {
            throw new InvalidOperationException("Fighter level 11 can only be created from fighter level 10");
        }

        return await CreateLevelInstance(Levels.Fighter11, null, fighterLevel10.ClassInstance, fighterLevel10.SubclassInstance, []);
    }

    public static async Task<ILevelInstance> FighterLevel12(ILevelInstance fighterLevel11, IFeatureInstance chosenFeature)
    {
        if (fighterLevel11.LevelModel.Url != Levels.Fighter11)
        {
            throw new InvalidOperationException("Fighter level 12 can only be created from fighter level 11");
        }

        return await CreateLevelInstance(Levels.Fighter12, null, fighterLevel11.ClassInstance, fighterLevel11.SubclassInstance, [chosenFeature]);
    }

    public static async Task<ILevelInstance> FighterLevel13(ILevelInstance fighterLevel12)
    {
        if (fighterLevel12.LevelModel.Url != Levels.Fighter12)
        {
            throw new InvalidOperationException("Fighter level 13 can only be created from fighter level 12");
        }

        return await CreateLevelInstance(Levels.Fighter13, null, fighterLevel12.ClassInstance, fighterLevel12.SubclassInstance, []);
    }

    public static async Task<ILevelInstance> FighterLevel14(ILevelInstance fighterLevel13, IFeatureInstance chosenFeature)
    {
        if (fighterLevel13.LevelModel.Url != Levels.Fighter13)
        {
            throw new InvalidOperationException("Fighter level 14 can only be created from fighter level 13");
        }

        return await CreateLevelInstance(Levels.Fighter14, null, fighterLevel13.ClassInstance, fighterLevel13.SubclassInstance, [chosenFeature]);
    }

    public static async Task<ILevelInstance> FighterLevel15Champion(ILevelInstance fighterLevel14)
    {
        if (fighterLevel14.LevelModel.Url != Levels.Fighter14)
        {
            throw new InvalidOperationException("Fighter level 15 can only be created from fighter level 14");
        }

        if (fighterLevel14.SubclassInstance?.SubclassModel.Url != Subclasses.Champion)
        {
            throw new InvalidOperationException("Champion level 15 can only be created from champion level 14");
        }

        return await CreateLevelInstance(Levels.Fighter15, SubclassLevels.Champion15, fighterLevel14.ClassInstance, fighterLevel14.SubclassInstance, []);
    }

    public static async Task<ILevelInstance> FighterLevel16(ILevelInstance fighterLevel15, IFeatureInstance chosenFeature)
    {
        if (fighterLevel15.LevelModel.Url != Levels.Fighter15)
        {
            throw new InvalidOperationException("Fighter level 16 can only be created from fighter level 15");
        }

        return await CreateLevelInstance(Levels.Fighter16, null, fighterLevel15.ClassInstance, fighterLevel15.SubclassInstance, [chosenFeature]);
    }

    public static async Task<ILevelInstance> FighterLevel17(ILevelInstance fighterLevel16)
    {
        if (fighterLevel16.LevelModel.Url != Levels.Fighter16)
        {
            throw new InvalidOperationException("Fighter level 17 can only be created from fighter level 16");
        }

        return await CreateLevelInstance(Levels.Fighter17, null, fighterLevel16.ClassInstance, fighterLevel16.SubclassInstance, []);
    }

    public static async Task<ILevelInstance> FighterLevel18Champion(ILevelInstance fighterLevel17)
    {
        if (fighterLevel17.LevelModel.Url != Levels.Fighter17)
        {
            throw new InvalidOperationException("Fighter level 18 can only be created from fighter level 17");
        }

        if (fighterLevel17.SubclassInstance?.SubclassModel.Url != Subclasses.Champion)
        {
            throw new InvalidOperationException("Champion level 18 can only be created from champion level 17");
        }

        return await CreateLevelInstance(Levels.Fighter18, SubclassLevels.Champion18, fighterLevel17.ClassInstance, fighterLevel17.SubclassInstance, [await Survivor.Create()]);
    }

    public static async Task<ILevelInstance> FighterLevel19(ILevelInstance fighterLevel18, IFeatureInstance chosenFeature)
    {
        if (fighterLevel18.LevelModel.Url != Levels.Fighter18)
        {
            throw new InvalidOperationException("Fighter level 19 can only be created from fighter level 18");
        }

        return await CreateLevelInstance(Levels.Fighter19, null, fighterLevel18.ClassInstance, fighterLevel18.SubclassInstance, [chosenFeature]);
    }

    public static async Task<ILevelInstance> FighterLevel20(ILevelInstance fighterLevel19)
    {
        if (fighterLevel19.LevelModel.Url != Levels.Fighter19)
        {
            throw new InvalidOperationException("Fighter level 20 can only be created from fighter level 19");
        }

        return await CreateLevelInstance(Levels.Fighter20, null, fighterLevel19.ClassInstance, fighterLevel19.SubclassInstance, []);
    }
}
