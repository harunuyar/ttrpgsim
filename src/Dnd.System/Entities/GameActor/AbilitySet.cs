namespace Dnd.System.Entities.GameActor;

using Dnd._5eSRD.Models.AbilityScore;

public class AbilitySet
{
    public AbilitySet()
    {
        Scores = [];
    }

    public Dictionary<AbilityScoreModel, int> Scores { get; }

    public void AddAbilityScore(AbilityScoreModel reference, int value)
    {
        Scores.Add(reference, value);
    }

    public int GetAbilityScore(AbilityScoreModel reference)
    {
        return Scores.GetValueOrDefault(reference, 0);
    }
}
