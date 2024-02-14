namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.Entities;
using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Spells;

public class CalculateSpellAttackModifier : DndScoreCommand
{
    public CalculateSpellAttackModifier(IGameActor character, ISpell spell, IGameActor target) : base(character)
    {
        Spell = spell;
        Target = target;
    }

    public ISpell Spell { get; }

    public IGameActor Target { get; }

    protected override void InitializeResult()
    {
        if (Spell.SuccessMeasuringType != ESuccessMeasuringType.AttackRoll)
        {
            SetErrorAndReturn("Spell doesn't use attack roll");
            return;
        }

        // Real base value for spell attack modifier will be provided by spell casting ability feat if there is one
        Result.SetBaseValue("Base", 0);
    }
}
