namespace Dnd.Context;

public interface ILogger
{
    void Log(string message);
    void LogError(string message);
}
