namespace Dnd.Context;

public class CallbackLogger : ILogger
{
    public List<Action<string>> MessageCallbacks { get; }
    public List<Action<string>> ErrorCallbacks { get; }

    public CallbackLogger()
    {
        MessageCallbacks = [Console.WriteLine];
        ErrorCallbacks = [Console.Error.WriteLine];
    }

    public void Log(string message)
    {
        MessageCallbacks.ForEach(callback => callback(message));
    }

    public void LogError(string message)
    {
        ErrorCallbacks.ForEach(callback => callback(message));
    }
}
