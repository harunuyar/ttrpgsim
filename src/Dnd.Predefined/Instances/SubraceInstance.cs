namespace Dnd.Predefined.Instances;

using Dnd._5eSRD.Models.Language;
using Dnd._5eSRD.Models.Subrace;
using Dnd.Predefined.Commands.BoolCommands;
using Dnd.Predefined.Commands.ListCommands;
using Dnd.Predefined.Commands.ScoreCommands;
using Dnd.Predefined.ModelExtensions;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Instances;

public class SubraceInstance : ISubraceInstance
{
    public SubraceInstance(SubraceModel subraceModel, IEnumerable<LanguageModel> languageOptions, IEnumerable<ITraitInstance> racialTraits)
    {
        SubraceModel = subraceModel;
        LanguageOptions = languageOptions.ToList();
        RacialTraits = racialTraits.ToList();
    }

    public SubraceModel SubraceModel { get; }

    public List<LanguageModel> LanguageOptions { get; }

    public List<ITraitInstance> RacialTraits { get; }

    public async Task HandleCommand(ICommand command)
    {
        foreach (var trait in RacialTraits)
        {
            await trait.HandleCommand(command);
        }

        if (command is GetSpokenLanguages languages)
        {
            languages.AddValues(LanguageOptions, "Subrace Language Options");
        }
        else if (command is GetTotalAbilityScore totalAbilityScore)
        {
            foreach (var ability in SubraceModel.AbilityBonuses ?? [])
            {
                if (ability.AbilityScore?.Url == totalAbilityScore.AbilityScore.Url && ability.Bonus.HasValue)
                {
                    totalAbilityScore.AddBonus(ability.Bonus.Value, SubraceModel.Name ?? "Ability Bonus");
                    return;
                }
            }
        }
        else if (command is HasProficiency hasProficiency)
        {
            foreach (var proficiency in SubraceModel.StartingProficiencies ?? [])
            {
                if (await proficiency.HasProficiency(hasProficiency.ProficiencyReference))
                {
                    hasProficiency.SetValue(true, SubraceModel.Name ?? "Proficiency");
                    return;
                }
            }
        }
    }
}
