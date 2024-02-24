namespace Dnd.System.Entities.Units;

public class TimeSpan
{
    public TimeSpan(int turns)
    {
        Value = global::System.TimeSpan.FromSeconds(turns * 6);
    }

    public TimeSpan(global::System.TimeSpan value)
    {
        Value = value;
    }

    public global::System.TimeSpan Value { get; private set; }

    public int Turns => (int)Math.Ceiling(Value.TotalSeconds / 6);

    public void PassTime(global::System.TimeSpan time)
    {
        Value = Value.Subtract(time);
    }

    public void PassTime(TimeSpan time)
    {
        PassTime(time.Value);
    }

    public void PassTurn()
    {
        Value = Value.Subtract(global::System.TimeSpan.FromSeconds(6));
    }

    public bool IsOver => Value.TotalSeconds <= 0;
}
