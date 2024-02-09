namespace DnD.CommandSystem.Commands;

using DnD.CommandSystem.Results;
using DnD.Entities.Characters;

internal abstract class DndBooleanCommand : DndCommand
{
    public DndBooleanCommand(Character character) : base(character)
    {
        BooleanBonuses = new BooleanBonuses(this);
    }

    public BooleanBonuses BooleanBonuses { get; }

    public abstract override BooleanResult Execute();

    public override EventResult IsValid() => EventResult.Success(this);
}
