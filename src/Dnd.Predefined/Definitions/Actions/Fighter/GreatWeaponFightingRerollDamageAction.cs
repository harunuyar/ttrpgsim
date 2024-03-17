namespace Dnd.Predefined.Definitions.Actions.Fighter;

using Dnd._5eSRD.Constants;
using Dnd.Predefined.Actions;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;

public class GreatWeaponFightingRerollDamageAction : EventReaction
{
    public GreatWeaponFightingRerollDamageAction() 
        : base("Great Weapon Fighting: Reroll Damage", ActionDurationType.FreeAction, [], false)
    {
    }

    private static readonly int[] RerollResults = [1, 2];

    public override Task<bool> IsReactionAvailable(IGameActor gameActor, IEvent eventToReactTo)
    {
        return Task.FromResult(
            gameActor == eventToReactTo.EventOwner
            && eventToReactTo is IAmountRollEvent damageRoll
            && damageRoll.RawRollResults is not null
            && RerollResults.Contains(damageRoll.RawRollResults.Select(x => x.Result).DefaultIfEmpty(0).Sum())
            && damageRoll.AmountAction is IWeaponAttackAction weaponAttack
            && (weaponAttack.HandType == EAttackHandType.Versatile
                || (weaponAttack.HandType == EAttackHandType.MainHand
                    && (weaponAttack.Weapon.EquipmentModel.Properties?.Any(x => x.Url == WeaponProperties.TwoHanded) ?? false))));
    }

    public override async Task<IEvent> CreateReactionEvent(IGameActor actor, IEvent eventToReactTo)
    {
        if (await IsReactionAvailable(actor, eventToReactTo))
        {
            throw new InvalidOperationException("Reaction is not available");
        }

        if (eventToReactTo is not IAmountRollEvent amountRollEvent)
        {
            throw new InvalidOperationException("GreatWeaponFightingRerollDamageAction can only react to amount roll events");
        }

        if (amountRollEvent.RawRollResults is null || amountRollEvent.ModifierRollResults is null)
        {
            throw new InvalidOperationException("There is no roll dice to reroll");
        }

        var reroll = amountRollEvent.CreateReRollEvent(amountRollEvent.RawRollResults, amountRollEvent.ModifierRollResults);

        return reroll;
    }
}
