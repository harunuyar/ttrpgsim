namespace Dnd.System.Entities.Instances;

using Dnd._5eSRD.Models.Language;
using Dnd._5eSRD.Models.Proficiency;
using Dnd._5eSRD.Models.Race;

public interface IRaceInstance : ICommandHandler
{
    RaceModel RaceModel { get; }
    List<RaceAbilityBonusModel> AbilityBonusOptions { get; }
    List<LanguageModel> LanguageOptions { get; }
    List<ProficiencyModel> StartingProficiencyOptions { get; }
    List<ITraitInstance> Traits { get; }
}
