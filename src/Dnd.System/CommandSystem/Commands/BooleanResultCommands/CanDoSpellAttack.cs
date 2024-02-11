namespace Dnd.System.CommandSystem.Commands.BooleanResultCommands;

using Dnd.System.Entities.Characters;
using Dnd.System.Entities.Spells;

public class CanDoSpellAttack : DndBooleanCommand
{
    public CanDoSpellAttack(ICharacter character, ISpell spell, ICharacter target) : base(character)
    {
        Spell = spell;
        Target = target;
    }

    public ISpell Spell { get; set; }

    public ICharacter Target { get; set; }

    public override void InitializeResult()
    {
        Result.SetValue("Default", true);
    }
}
