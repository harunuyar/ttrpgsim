namespace DnD.Entities;

using DnD.Commands;
using TableTopRpg.Commands;

internal interface ICommandInterrupter
{
    ICommandResult InterruptCommand(DndCommand command, ICommandResult currentResult);
}
