namespace Dnd.System.Entities.Units;

public class Duration
{
    private Duration(int turns)
    {
        Value = TimeSpan.FromSeconds(turns * 6);
    }

    private Duration(TimeSpan value)
    {
        Value = value;
    }

    public TimeSpan Value { get; private set; }

    public int Turns => (int)Math.Ceiling(Value.TotalSeconds / 6);

    public void PassTime(TimeSpan time)
    {
        Value = Value.Subtract(time);
    }

    public void PassTime(Duration time)
    {
        PassTime(time.Value);
    }

    public void PassTurn()
    {
        Value = Value.Subtract(TimeSpan.FromSeconds(6));
    }

    public bool IsOver => Value.TotalSeconds <= 0;

    public Duration Clone() => new Duration(Value);

    public static Duration OfTurns(int turns) => new Duration(turns);

    public static Duration OfTimeSpan(TimeSpan timeSpan) => new Duration(timeSpan);

    public static Duration? FromString(string? durationString)
    {
        if (string.IsNullOrWhiteSpace(durationString))
        {
            return null;
        }

        if (durationString.Contains("minute"))
        {
            string durationStringWithoutMinutes = durationString.Replace(" minutes", "").Replace(" minute", "");
            if (int.TryParse(durationStringWithoutMinutes, out int duration))
            {
                return OfTimeSpan(TimeSpan.FromMinutes(duration));
            }
        }
        else if (durationString.Contains("hour"))
        {
            string durationStringWithoutHours = durationString.Replace(" hours", "").Replace(" hour", "");
            if (int.TryParse(durationStringWithoutHours, out int duration))
            {
                return OfTimeSpan(TimeSpan.FromHours(duration));
            }
        }
        else if (durationString.Contains("day"))
        {
            string durationStringWithoutDays = durationString.Replace(" days", "").Replace(" day", "");
            if (int.TryParse(durationStringWithoutDays, out int duration))
            {
                return OfTimeSpan(TimeSpan.FromDays(duration));
            }
        }
        else if (durationString.Contains("round"))
        {
            string durationStringWithoutDays = durationString.Replace(" rounds", "").Replace(" round", "");
            if (int.TryParse(durationStringWithoutDays, out int duration))
            {
                return OfTurns(duration);
            }
        }

        return null;
    }
}
