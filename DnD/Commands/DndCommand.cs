namespace DnD.Commands;

using DnD.Entities;
using DnD.Entities.Characters;
using TableTopRpg.Commands;

internal abstract class DndCommand : ICommand
{
    public DndCommand(Character character)
    {
        Character = character;
    }

    public Character Character { get; }

    public ICommandResult Execute()
    {
        ICommandResult result = ExecuteDndCommand();

        foreach (var trait in Character.Traits)
        {
            if (trait is ICommandInterrupter interrupter)
            {
                result = interrupter.InterruptCommand(this, result);
            }
        }

        foreach (var item in Character.Inventory.Items)
        {
            if (item.ItemDescription is ICommandInterrupter interrupter)
            {
                result = interrupter.InterruptCommand(this, result);
            }
        }

        foreach (var effect in Character.Effects)
        {
            if (effect is ICommandInterrupter interrupter)
            {
                result = interrupter.InterruptCommand(this, result);
            }
        }

        return result;
    }

    protected abstract ICommandResult ExecuteDndCommand();
}
