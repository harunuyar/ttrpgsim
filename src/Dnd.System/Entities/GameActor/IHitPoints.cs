namespace Dnd.System.Entities.GameActor;

public interface IHitPoints : ICommandHandler
{
    int CurrentHitPoints { get; }
    int TemporaryHitPoints { get; }
    void Heal(int amount);
    void Damage(int amount);
    void SetCurrentHitPoints(int amount);
    void AddTemporaryHitPoints(int amount);
    void RemoveTemporaryHitPoints(int amount);
    void SetTemporaryHitPoints(int amount);
    void AddHitPointRoll(int roll);
    int GetTotalHitPointRolls();
}
