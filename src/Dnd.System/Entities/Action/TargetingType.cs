namespace Dnd.System.Entities.Action;

using Dnd._5eSRD.Models.Common;
using Dnd.System.Entities.GameActor;

public enum ETargetingType
{
    SingleTarget,
    MultiTarget,
    AreaOfEffect
}

public class TargetingType
{
    private TargetingType(ETargetingType type, int? targetCount, bool uniqueTargets, AreaOfEffectModel? areaOfEffect)
    {
        Type = type;
        TargetCount = targetCount;
        AreaOfEffectModel = areaOfEffect;
        UniqueTargets = uniqueTargets;
    }

    public ETargetingType Type { get; }

    public int? TargetCount { get; }

    public AreaOfEffectModel? AreaOfEffectModel { get; }

    public bool UniqueTargets { get; }

    public bool IsSuitable(IEnumerable<IGameActor> targets)
    {
        if (Type == ETargetingType.SingleTarget)
        {
            return targets.Count() == 1;
        }

        if (Type == ETargetingType.MultiTarget)
        {
            if (UniqueTargets)
            {
                return targets.Distinct().Count() == TargetCount;
            }
            else
            {
                return targets.Count() == TargetCount;
            }
        }

        return true;
    }

    public static TargetingType SingleTarget => new(ETargetingType.SingleTarget, 1, true, null);

    public static TargetingType MultiTarget(int targetCount, bool uniqueTargets) => new(ETargetingType.MultiTarget, targetCount, uniqueTargets, null);

    public static TargetingType AreaOfEffect(AreaOfEffectModel areaOfEffect) => new(ETargetingType.AreaOfEffect, null, true, areaOfEffect);
}
