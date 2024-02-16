namespace Dnd.System.Events;

using Dnd.System.CommandSystem.Commands.BooleanResultCommands;
using Dnd.System.CommandSystem.Commands.EventCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities.GameActors;
using Dnd.System.Events.EventListener;

public class HealEvent : AEvent
{
    private BooleanResult? canHealResult;
    private IntegerResultWithBonus? healAmountResult;

    public HealEvent(IEventListener eventListener, IGameActor actor, int amount) : base(eventListener)
    {
        Actor = actor;
        Amount = amount;
    }

    public IGameActor Actor { get; }

    public int Amount { get; }

    public override BooleanResult IsValid()
    {
        if (this.canHealResult != null)
        {
            return this.canHealResult;
        }

        var canHeal = new CanHeal(Actor);
        var canHealResult = canHeal.Execute();

        if (!canHealResult.IsSuccess)
        {
            return BooleanResult.Failure("CanHeal: " + canHealResult.ErrorMessage);
        }

        this.canHealResult = canHealResult;

        return canHealResult;
    }

    public IntegerResultWithBonus CalculateHealAmountWithModifiers()
    {
        if (this.healAmountResult != null)
        {
            return this.healAmountResult;
        }

        var calculateHeal = new CalculateHealAmount(Actor, Amount);
        var healAmountResult = calculateHeal.Execute();

        if (!healAmountResult.IsSuccess)
        {
            return IntegerResultWithBonus.Failure("CalculateHealAmount: " + healAmountResult.ErrorMessage);
        }

        this.healAmountResult = healAmountResult;
        return healAmountResult;
    }

    public EventResult Heal()
    {
        var canHeal = IsValid();

        if (!canHeal.IsSuccess)
        {
            return EventResult.Failure("CanHeal: " + canHeal.ErrorMessage);
        }

        if (!canHeal.Value)
        {
            return EventResult.Failure($"{canHeal.Source}: {Actor.Name} can not be healed.");
        }

        var amountWithModifiers = CalculateHealAmountWithModifiers();
        if (!amountWithModifiers.IsSuccess)
        {
            return EventResult.Failure("CalculateHealAmountWithModifiers: " + amountWithModifiers.ErrorMessage);
        }

        var heal = new ApplyHeal(EventListener, Actor, amountWithModifiers.Value);
        return heal.Execute();
    }

    public override EventResult QuickRun() => Heal();
}
