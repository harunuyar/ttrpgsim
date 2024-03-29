﻿namespace Dnd.Predefined.Instances;

using Dnd._5eSRD.Models.Language;
using Dnd._5eSRD.Models.Proficiency;
using Dnd._5eSRD.Models.Spell;
using Dnd._5eSRD.Models.Trait;
using Dnd.Predefined.Commands.BoolCommands;
using Dnd.Predefined.Commands.ListCommands;
using Dnd.Predefined.ModelExtensions;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Instances;

public class TraitInstance : ITraitInstance
{
    public TraitInstance(TraitModel traitModel, IEnumerable<ProficiencyModel> proficiencyChoices, IEnumerable<LanguageModel> languageOptions, IEnumerable<ITraitInstance> subtraitOptions, IEnumerable<SpellModel> spellOptions)
    {
        TraitModel = traitModel;
        ProficiencyChoices = proficiencyChoices.ToList();
        LanguageOptions = languageOptions.ToList();
        SubtraitOptions = subtraitOptions.ToList();
        SpellOptions = spellOptions.ToList();
    }

    public TraitModel TraitModel { get; }

    public List<ITraitInstance> SubtraitOptions { get; }

    public List<SpellModel> SpellOptions { get; }

    public List<ProficiencyModel> ProficiencyChoices { get; }

    public List<LanguageModel> LanguageOptions { get; }

    public virtual async Task HandleCommand(ICommand command)
    {
        foreach (var subtrait in SubtraitOptions)
        {
            await subtrait.HandleCommand(command);
        }

        if (command is HasProficiency hasProficiency)
        {
            foreach (var proficiency in ProficiencyChoices)
            {
                if (await proficiency.HasProficiency(hasProficiency.ProficiencyReference))
                {
                    hasProficiency.SetValue(true, TraitModel.Name ?? "Proficiency");
                    return;
                }
            }

            foreach (var proficiency in TraitModel.Proficiencies ?? [])
            {
                if (await proficiency.HasProficiency(hasProficiency.ProficiencyReference))
                {
                    hasProficiency.SetValue(true, TraitModel.Name ?? "Proficiency");
                    return;
                }
            }
        }
        else if (command is GetSpokenLanguages languages)
        {
            languages.AddValues(LanguageOptions, $"{TraitModel.Name} Language Options");
        }
    }
}