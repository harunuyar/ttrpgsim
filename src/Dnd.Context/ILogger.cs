namespace Dnd.Context;

public interface ILogger
{
    Task Log(string message);
    Task LogError(string message);
}
