namespace Dnd.Predefined.Events;

using Dnd.Predefined.Commands.ValueCommands;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class SavingThrowDamageEvent : SavingThrowEvent
{
    public SavingThrowDamageEvent(string name, IGameActor eventOwner, ISavingThrowDamageAction savingThrowDamageAction, int? damage, IGameActor? target)
        : base(name, eventOwner, savingThrowDamageAction, target)
    {
        SavingThrowDamageAction = savingThrowDamageAction;
        Damage = damage;
    }

    public override bool IsWaitingForUserInput => base.IsWaitingForUserInput || Damage is null;

    public ISavingThrowDamageAction SavingThrowDamageAction { get; }

    public int? Damage { get; }

    public double? SaveSuccessDamageMultiplier { get; private set; }

    public double? SaveFailureDamageMultiplier { get; private set; }

    public override async Task<IEnumerable<IEvent>> RunEventImpl()
    {
        if (SaveSuccessDamageMultiplier is null || SaveFailureDamageMultiplier is null)
        {
            var successMultiplier = await new GetSaveSuccessDamageMultiplier(Target!, SavingThrowDamageAction).Execute();

            if (!successMultiplier.IsSuccess)
            {
                throw new InvalidOperationException("Failed to get save success damage multiplier");
            }

            SaveSuccessDamageMultiplier = successMultiplier.Value;

            var failureMultiplier = await new GetSaveFailureDamageMultiplier(Target!, SavingThrowDamageAction).Execute();

            if (!failureMultiplier.IsSuccess)
            {
                throw new InvalidOperationException("Failed to get save failure damage multiplier");
            }

            SaveFailureDamageMultiplier = failureMultiplier.Value;
        }

        return await base.RunEventImpl();
    }

    public override Task<IEvent?> GetAttackEvent(ERollResult rollResult, IGameActor target)
    {
        if (rollResult.IsSuccess())
        {
            double multiplier = SaveSuccessDamageMultiplier ?? throw new InvalidOperationException("SaveSuccessDamageMultiplier is not set");
            return Task.FromResult<IEvent?>(new DamageEvent($"{EventName}: Damage", target, SavingThrowDamageAction.DamageType, (int)(Damage! * multiplier)));
        }
        else
        {
            double multiplier = SaveFailureDamageMultiplier ?? throw new InvalidOperationException("SaveFailureDamageMultiplier is not set");
            return Task.FromResult<IEvent?>(new DamageEvent($"{EventName}: Damage", target, SavingThrowDamageAction.DamageType, (int)(Damage! * multiplier)));
        }
    }
}
