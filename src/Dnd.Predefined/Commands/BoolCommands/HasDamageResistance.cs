namespace Dnd.Predefined.Commands.BoolCommands;

using Dnd._5eSRD.Models.DamageType;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class HasDamageResistance : ValueCommand<bool>
{
    public HasDamageResistance(IGameActor character, DamageTypeModel damageType) : base(character)
    {
        DamageType = damageType;
    }

    public DamageTypeModel DamageType { get; }

    protected override Task InitializeResult()
    {
        SetValue(false, $"{Actor.Name} doesn't have resistance to {DamageType}");

        return Task.CompletedTask;
    }
}
