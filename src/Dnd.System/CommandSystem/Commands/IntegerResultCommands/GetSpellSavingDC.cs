namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.Entities;
using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Spells;

public class GetSpellSavingDC : DndScoreCommand
{
    public GetSpellSavingDC(IGameActor character, ISpell spell) : base(character)
    {
        Spell = spell;
    }

    public ISpell Spell { get; }

    protected override void InitializeResult()
    {
        if (Spell.SuccessMeasuringType == ESuccessMeasuringType.SavingThrow)
        {
            Result.SetBaseValue("Base", 8);
        }
        else
        {
            SetErrorAndReturn("Spell doesn't require saving throw");
        }
    }
}
