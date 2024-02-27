namespace Dnd.Predefined.Commands.ScoreCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;

public class GetConcentrationSavingDC : ValueCommand<int>
{
    public GetConcentrationSavingDC(IGameActor character, IAttackAction attackAction, int damageTaken) : base(character)
    {
        AttackAction = attackAction;
        DamageTaken = damageTaken;
    }

    public IAttackAction AttackAction { get; }

    public int DamageTaken { get; set; }

    protected override Task InitializeResult()
    {
        int damageDC = DamageTaken / 2;
        if (damageDC > 10)
        {
            SetValue(damageDC, "Half of DamageBonusCommands Taken");
        }
        else
        {
            SetValue(10, "Base");
        }

        return Task.CompletedTask;
    }
}
