namespace DnD.CommandSystem.Commands;

using DnD.CommandSystem.Results;
using DnD.Entities.Characters;

internal abstract class DndScoreCommand : DndCommand
{
    public DndScoreCommand(Character character) : base(character)
    {
        IntegerBonuses = new IntegerBonuses(this);
    }

    public IntegerBonuses IntegerBonuses { get; }

    public override EventResult IsValid() => EventResult.Success(this);

    public abstract override IntegerResult Execute();
}
