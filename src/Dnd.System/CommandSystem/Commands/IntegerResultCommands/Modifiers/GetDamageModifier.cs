namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands.Modifiers;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.Entities.Actions.BaseActions;
using Dnd.System.Entities.GameActors;

public class GetDamageModifier : DndScoreCommand
{
    public GetDamageModifier(IGameActor character, IGameActor? target, IAttackAction attackAction) : base(character)
    {
        Target = target;
        AttackAction = attackAction;
    }

    public IGameActor? Target { get; }

    public IAttackAction AttackAction { get; }

    protected override void InitializeResult()
    {
        Result.SetBaseValue("Base", 0);

        AttackAction.HandleCommand(this);

        if (Target != null)
        {
            var damageModifierAgainst = new GetDamageModifierAgainst(Target, Actor, AttackAction).Execute();

            if (!damageModifierAgainst.IsSuccess)
            {
                SetErrorAndReturn("GetDamageModifierAgainst: " + damageModifierAgainst.ErrorMessage);
                return;
            }

            Result.AddAsBonus("Damage Modifier From Target", damageModifierAgainst);
        }
    }
}
