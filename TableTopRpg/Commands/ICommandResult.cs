namespace TableTopRpg.Commands;

public interface ICommandResult
{
    ICommand Command { get; }
    bool IsSuccess { get; }
    string Message { get; }
}
