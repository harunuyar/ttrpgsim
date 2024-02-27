namespace Dnd.Predefined.GameActor;

using Dnd._5eSRD.Models.AbilityScore;
using Dnd.System.Entities.GameActor;

public class AbilitySet : IAbilitySet
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
