namespace Dnd.Predefined.Effects.Special;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.AbilityScore;
using Dnd._5eSRD.Models.Feature;
using Dnd.Context;
using Dnd.Predefined.Commands.ScoreCommands;
using Dnd.Predefined.Commands.VoidCommands;
using Dnd.Predefined.Events;
using Dnd.System.Entities.Effect;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;

public class SurvivorEffect : ActiveEffectDefinition
{
    public SurvivorEffect(FeatureModel survivorFeatureModel) : base(survivorFeatureModel.Name ?? "Survivor", string.Join(" ", survivorFeatureModel.Desc ?? []), EEffectActivationTime.TurnStart | EEffectActivationTime.Target)
    {
        SurvivorFeatureModel = survivorFeatureModel;
    }

    public FeatureModel SurvivorFeatureModel { get; }

    public override IEffectEvent CreateEvent(IGameActor source, IGameActor target)
    {
        return new BasicEffectEvent(this, source, target, new Task(async () =>
        {
            var maxHp = await new GetMaxHP(target).Execute();

            if (!maxHp.IsSuccess)
            {
                throw new InvalidOperationException("GetMaxHP: " + maxHp.ErrorMessage);
            }

            var currentHp = target.HitPoints.CurrentHitPoints;

            if (currentHp <= maxHp.Value / 2)
            {
                var constitution = await DndContext.Instance.GetObject<AbilityScoreModel>(AbilityScores.Con);
                if (constitution == null)
                {
                    throw new InvalidOperationException("Ability score model for " + AbilityScores.Con + " is not found");
                }

                var constitutionModifier = await new GetAbilityModifier(target, constitution).Execute();

                if (!constitutionModifier.IsSuccess)
                {
                    throw new InvalidOperationException("GetAbilityModifier: " + constitutionModifier.ErrorMessage);
                }

                int healAmount = constitutionModifier.Value + 5;

                var heal = await new ApplyHeal(target, healAmount).Execute();

                if (!heal.IsSuccess)
                {
                    throw new InvalidOperationException("ApplyHeal: " + heal.ErrorMessage);
                }
            }
        }));
    }
}
