namespace Dnd.Predefined.Instances;

using Dnd._5eSRD.Models.Language;
using Dnd._5eSRD.Models.Proficiency;
using Dnd._5eSRD.Models.Race;
using Dnd.Predefined.Commands.BoolCommands;
using Dnd.Predefined.Commands.ListCommands;
using Dnd.Predefined.Commands.ScoreCommands;
using Dnd.Predefined.ModelExtensions;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Instances;

public class RaceInstance : IRaceInstance
{
    public RaceInstance(
        RaceModel raceModel,
        IEnumerable<RaceAbilityBonusModel> abilityBonusOptions,
        IEnumerable<LanguageModel> languageOptions,
        IEnumerable<ProficiencyModel> startingProficiencyOptions,
        IEnumerable<ITraitInstance> traits)
    {
        RaceModel = raceModel;
        AbilityBonusOptions = abilityBonusOptions.ToList();
        LanguageOptions = languageOptions.ToList();
        StartingProficiencyOptions = startingProficiencyOptions.ToList();
        Traits = traits.ToList();
    }

    public RaceModel RaceModel { get; }

    public List<RaceAbilityBonusModel> AbilityBonusOptions { get; }

    public List<LanguageModel> LanguageOptions { get; }

    public List<ProficiencyModel> StartingProficiencyOptions { get; }

    public List<ITraitInstance> Traits { get; }

    public async Task HandleCommand(ICommand command)
    {
        foreach (var trait in Traits)
        {
            await trait.HandleCommand(command);
        }

        if (command is HasProficiency hasProficiency)
        {
            foreach (var proficiency in StartingProficiencyOptions)
            {
                if (await proficiency.HasProficiency(hasProficiency.ProficiencyReference))
                {
                    hasProficiency.SetValue(true, RaceModel.Name ?? "Proficiency");
                    return;
                }
            }

            foreach (var proficiency in RaceModel.StartingProficiencies ?? [])
            {
                if (await proficiency.HasProficiency(hasProficiency.ProficiencyReference))
                {
                    hasProficiency.SetValue(true, RaceModel.Name ?? "Proficiency");
                    return;
                }
            }
        }
        else if (command is GetTotalAbilityScore totalAbilityScore)
        {
            foreach (var ability in AbilityBonusOptions)
            {
                if (ability.AbilityScore?.Url == totalAbilityScore.AbilityScore.Url && ability.Bonus.HasValue)
                {
                    totalAbilityScore.AddBonus(ability.Bonus.Value, RaceModel.Name ?? "Ability Bonus");
                    return;
                }
            }

            foreach (var ability in RaceModel.AbilityBonuses ?? [])
            {
                if (ability.AbilityScore?.Url == totalAbilityScore.AbilityScore.Url && ability.Bonus.HasValue)
                {
                    totalAbilityScore.AddBonus(ability.Bonus.Value, RaceModel.Name ?? "Ability Bonus");
                    return;
                }
            }
        }
        else if (command is GetSpokenLanguages languages)
        {
            foreach (var lan in RaceModel.Languages ?? [])
            {
                var lanModel = await lan.GetModel<LanguageModel>();

                if (lanModel is null)
                {
                    languages.SetError("Language not found: " + lan.Url);
                    return;
                }

                languages.AddValue(lanModel, "Race");
            }

            languages.AddValues(LanguageOptions, "Race Language Options");
        }
    }
}
