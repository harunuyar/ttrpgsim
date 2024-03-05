namespace Dnd.Predefined.Definitions.Features.Fighter;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.Feature;
using Dnd.Context;
using Dnd.Predefined.Instances;

public class FightingStyle : FeatureInstance
{
    public static async Task<FightingStyle> Create(ISubFightingStyle subfeature)
    {
        var featureModel = await DndContext.Instance.GetObject<FeatureModel>(Features.FighterFightingStyle);
        return featureModel is null 
            ? throw new InvalidOperationException($"Couldn't find feature model {Features.FighterFightingStyle}") 
            : new FightingStyle(featureModel, subfeature);
    }

    private FightingStyle(FeatureModel featureModel, ISubFightingStyle subfeature)
        : base(featureModel, [subfeature], [], [])
    {
    }
}
