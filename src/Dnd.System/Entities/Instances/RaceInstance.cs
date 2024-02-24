namespace Dnd.System.Entities.Instances;

using Dnd._5eSRD.Models.AbilityScore;
using Dnd._5eSRD.Models.Language;
using Dnd._5eSRD.Models.Proficiency;
using Dnd._5eSRD.Models.Race;

public class RaceInstance
{
    public RaceInstance(
        RaceModel raceModel,
        IEnumerable<AbilityScoreModel> abilityBonusOptions,
        IEnumerable<LanguageModel> languageOptions,
        IEnumerable<ProficiencyModel> startingProficiencyOptions,
        IEnumerable<TraitInstance> traits)
    {
        RaceModel = raceModel;
        AbilityBonusOptions = abilityBonusOptions.ToList();
        LanguageOptions = languageOptions.ToList();
        StartingProficiencyOptions = startingProficiencyOptions.ToList();
        Traits = traits.ToList();
    }

    public RaceModel RaceModel { get; }

    public List<AbilityScoreModel> AbilityBonusOptions { get; }

    public List<LanguageModel> LanguageOptions { get; }

    public List<ProficiencyModel> StartingProficiencyOptions { get; }

    public List<TraitInstance> Traits { get; }

    public override bool Equals(object? obj)
    {
        return obj is RaceInstance raceInstance
            && raceInstance.RaceModel == RaceModel
            && raceInstance.AbilityBonusOptions.Equals(AbilityBonusOptions)
            && raceInstance.LanguageOptions.Equals(LanguageOptions)
            && raceInstance.StartingProficiencyOptions.Equals(StartingProficiencyOptions)
            && raceInstance.Traits.Equals(Traits);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(RaceModel, AbilityBonusOptions, LanguageOptions, StartingProficiencyOptions, Traits);
    }
}
