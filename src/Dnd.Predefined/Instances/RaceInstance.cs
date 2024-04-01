namespace Dnd.Predefined.Instances;

using Dnd._5eSRD.Models.Language;
using Dnd._5eSRD.Models.Proficiency;
using Dnd._5eSRD.Models.Race;
using Dnd.Predefined.Actions.ActionTypes;
using Dnd.Predefined.Commands.BoolCommands;
using Dnd.Predefined.Commands.ListCommands;
using Dnd.Predefined.Commands.ScoreCommands;
using Dnd.Predefined.ModelExtensions;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.Instances;

public class RaceInstance : IRaceInstance
{
    public static async Task<RaceInstance> Create(RaceModel raceModel,
        IEnumerable<RaceAbilityBonusModel> abilityBonusOptions,
        IEnumerable<LanguageModel> languageOptions,
        IEnumerable<ProficiencyModel> startingProficiencyOptions,
        IEnumerable<ITraitInstance> traits)
    {
        var main = await UnarmedAttackAction.CreateMainHandUnarmedAttack();
        var off = await UnarmedAttackAction.CreateOffHandUnarmedAttack();
        return new RaceInstance(raceModel, main, off, abilityBonusOptions, languageOptions, startingProficiencyOptions, traits);
    }

    private RaceInstance(
        RaceModel raceModel,
        IUnarmedAttackAction mainHandUnarmedAttackAction,
        IUnarmedAttackAction offHandUnarmedAttackAction,
        IEnumerable<RaceAbilityBonusModel> abilityBonusOptions,
        IEnumerable<LanguageModel> languageOptions,
        IEnumerable<ProficiencyModel> startingProficiencyOptions,
        IEnumerable<ITraitInstance> traits)
    {
        RaceModel = raceModel;
        MainHandUnarmedAttackAction = mainHandUnarmedAttackAction;
        OffHandUnarmedAttackAction = offHandUnarmedAttackAction;
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

    public IUnarmedAttackAction MainHandUnarmedAttackAction { get; }

    public IUnarmedAttackAction OffHandUnarmedAttackAction { get; }

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
        else if (command is GetBaseAbilityScore baseAbilityScore)
        {
            foreach (var ability in AbilityBonusOptions)
            {
                if (ability.AbilityScore?.Url == baseAbilityScore.Ability.Url && ability.Bonus.HasValue)
                {
                    baseAbilityScore.AddBonus(ability.Bonus.Value, RaceModel.Name ?? "Ability Bonus");
                    return;
                }
            }

            foreach (var ability in RaceModel.AbilityBonuses ?? [])
            {
                if (ability.AbilityScore?.Url == baseAbilityScore.Ability.Url && ability.Bonus.HasValue)
                {
                    baseAbilityScore.AddBonus(ability.Bonus.Value, RaceModel.Name ?? "Ability Bonus");
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
        else if (command is GetActions actions)
        {
            actions.AddValue(MainHandUnarmedAttackAction, "Main Hand Unarmed Attack");
            actions.AddValue(OffHandUnarmedAttackAction, "Off Hand Unarmed Attack");
        }
    }
}
