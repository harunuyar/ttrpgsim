namespace Dnd.Predefined.Definitions.Actions.Fighter;

using Dnd._5eSRD.Constants;
using Dnd.Predefined.Actions;
using Dnd.Predefined.Events;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;

public class GreatWeaponFightingRerollDamageAction : Reaction, IRerollAction
{
    public GreatWeaponFightingRerollDamageAction() 
        : base("Great Weapon Fighting: Reroll Damage", ActionDurationType.FreeAction, [], EReactionType.After | EReactionType.Self | EReactionType.DamageRoll, false)
    {
    }

    public ERollType RollType => ERollType.Damage;

    public override async Task<bool> IsReactionAvailable(IGameActor gameActor, IActionEvent eventToReactTo, EReactionType reactionType)
    {
        return await base.IsReactionAvailable(gameActor, eventToReactTo, reactionType)
            && eventToReactTo is IAmountRollEvent damageRoll
            && (damageRoll.RawAmountResult == 1 || damageRoll.RawAmountResult == 2)
            && damageRoll.Action is IWeaponAttackAction weaponAttack
            && (weaponAttack.HandType == EAttackHandType.Versatile
                || (weaponAttack.HandType == EAttackHandType.MainHand
                    && (weaponAttack.Weapon.EquipmentModel.Properties?.Any(x => x.Url == WeaponProperties.TwoHanded) ?? false)));
    }

    public override IActionEvent CreateReactionEvent(IGameActor actor, IActionEvent eventToReactTo)
    {
        Task task = new(() =>
        {
            if (eventToReactTo is IAmountRollEvent amountEvent)
            {
                amountEvent.ResetAmountRoll();
            }
        });

        return new BasicReactionEvent(actor, eventToReactTo, this, task);
    }
}
