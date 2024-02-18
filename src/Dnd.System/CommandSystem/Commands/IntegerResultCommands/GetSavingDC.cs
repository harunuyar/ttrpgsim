namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.Entities.Actions.BaseActions;
using Dnd.System.Entities.GameActors;

public class GetSavingDC : DndScoreCommand
{
    public GetSavingDC(IGameActor character, IAttackAction attackAction) : base(character)
    {
        AttackAction = attackAction;
    }

    public IAttackAction AttackAction { get; }

    protected override void InitializeResult()
    {
        Result.SetBaseValue("Base", 8);

        AttackAction.HandleCommand(this);
    }
}
