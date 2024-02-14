namespace Dnd.System.CommandSystem.Commands.BooleanResultCommands;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Spells;

public class CanDoSpellAttack : DndBooleanCommand
{
    public CanDoSpellAttack(IGameActor character, ISpell spell, IGameActor target) : base(character)
    {
        Spell = spell;
        Target = target;
    }

    public ISpell Spell { get; set; }

    public IGameActor Target { get; set; }

    protected override void InitializeResult()
    {
        Result.SetValue("Default", true);
    }
}
