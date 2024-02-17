namespace Dnd.Predefined.Effects.Conditions;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Effects.Duration;
using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.CommandSystem.Commands.BooleanResultCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands.Modifiers;
using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.DiceModifiers;

public class Paralyzed : AEffect
{
    public Paralyzed(IEffectDuration duration, IGameActor source, IGameActor target)
        : base("Paralyzed", "A paralyzed creature is incapacitated and can't move or speak. The creature automatically fails Strength and Dexterity saving throws. Attack rolls against the creature have advantage. Any attack that hits the creature is a critical hit if the attacker is within 5 feet of the creature.", 
            duration, source, target)
    {
    }

    public override void HandleCommand(ICommand command)
    {
        if (command is CanTakeAnyAction canTakeAnyAction)
        {
            canTakeAnyAction.SetValueAndReturn(this, false, "You are paralyzed and can't take any action.");
        }
        else if (command is GetSavingThrowModifier getSavingThrowModifier)
        {
            if ((getSavingThrowModifier.AttributeType & (EAttributeType.Strength | EAttributeType.Dexterity)) != 0)
            {
                getSavingThrowModifier.SetAutoFailure(this);
            }
        }
        else if (command is GetAttackModifierAgainst getAttackModifierAgainst)
        {
            getAttackModifierAgainst.AddAdvantage(this, EAdvantage.Advantage);

            // Accept all hits as critical hits regardless of the distance being within 5 feet.
            getAttackModifierAgainst.Result.BonusCollection.AddHitResult(this, EHitResult.CriticalHit);
        }
    }
}
