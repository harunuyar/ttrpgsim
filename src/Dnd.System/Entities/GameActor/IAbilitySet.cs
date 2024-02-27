namespace Dnd.System.Entities.GameActor;

using Dnd._5eSRD.Models.AbilityScore;

public interface IAbilitySet
{
    void AddAbilityScore(AbilityScoreModel reference, int value);
    int GetAbilityScore(AbilityScoreModel reference);
}
