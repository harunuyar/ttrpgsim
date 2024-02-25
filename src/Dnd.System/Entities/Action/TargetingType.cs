namespace Dnd.System.Entities.Action;

using Dnd._5eSRD.Models.Common;

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

    public static TargetingType SingleTarget => new(ETargetingType.SingleTarget, 1, true, null);

    public static TargetingType MultiTarget(int targetCount, bool uniqueTargets) => new(ETargetingType.MultiTarget, targetCount, uniqueTargets, null);

    public static TargetingType AreaOfEffect(AreaOfEffectModel areaOfEffect) => new(ETargetingType.AreaOfEffect, null, true, areaOfEffect);
}
