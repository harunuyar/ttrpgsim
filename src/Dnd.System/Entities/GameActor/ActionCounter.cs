namespace Dnd.System.Entities.GameActor;

public class ActionCounter
{
    public int ActionPoints { get; private set; }

    public int BonusActionPoints { get; private set; }

    public int ReactionPoints { get; private set; }

    public void Reset()
    {
        ActionPoints = 1;
        BonusActionPoints = 1;
        ReactionPoints = 1;
    }

    public void AddExtraActionPoint()
    {
        ActionPoints++;
    }

    public void UseActionPoint()
    {
        ActionPoints--;
    }

    public void UseBonusActionPoint()
    {
        BonusActionPoints--;
    }

    public void UseReactionPoint()
    {
        ReactionPoints--;
    }
}
