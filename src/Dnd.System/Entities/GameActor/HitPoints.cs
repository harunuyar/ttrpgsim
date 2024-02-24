namespace Dnd.System.Entities.GameActor;

public class HitPoints
{
    public HitPoints()
    {
        HitPointRolls = new List<int>();
        CurrentHitPoints = 0;
        TemporaryHitPoints = 0;
    }

    public List<int> HitPointRolls { get; }

    public int CurrentHitPoints { get; private set; }

    public int TemporaryHitPoints { get; private set; }

    internal int MaxHitPoints { get; set; }

    public void Heal(int amount)
    {
        if (amount < 0)
        {
            Damage(-amount);
        }
        else
        {
            CurrentHitPoints = Math.Min(CurrentHitPoints + amount, MaxHitPoints);
        }
    }

    public void Damage(int amount)
    {
        amount = Math.Max(amount, 0);
        int leftAmountAfterTemporary = Math.Max(amount - TemporaryHitPoints, 0);
        TemporaryHitPoints = Math.Max(TemporaryHitPoints - amount, 0);
        CurrentHitPoints = Math.Max(CurrentHitPoints - leftAmountAfterTemporary, 0);
    }

    public void SetCurrentHitPoints(int amount)
    {
        CurrentHitPoints = Math.Max(0, amount);
    }

    public void AddTemporaryHitPoints(int amount)
    {
        TemporaryHitPoints = Math.Max(0, TemporaryHitPoints + amount);
    }

    public void RemoveTemporaryHitPoints(int amount)
    {
        TemporaryHitPoints = Math.Max(0, TemporaryHitPoints - amount);
    }

    public void SetTemporaryHitPoints(int amount)
    {
        TemporaryHitPoints = Math.Max(0, amount);
    }

    public void SetMaxHitPoints(int amount)
    {
        amount = Math.Max(0, amount);

        if (CurrentHitPoints == MaxHitPoints)
        {
            CurrentHitPoints = amount;
        }

        MaxHitPoints = amount;
        CurrentHitPoints = Math.Min(CurrentHitPoints, MaxHitPoints);
    }

    public void AddHitPointRoll(int roll)
    {
        roll = Math.Max(0, roll);
        HitPointRolls.Add(roll);
    }
}
