namespace Dnd.System.Entities.Instances;

using Dnd._5eSRD.Models.Language;
using Dnd._5eSRD.Models.Proficiency;
using Dnd._5eSRD.Models.Spell;
using Dnd._5eSRD.Models.Trait;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities;

public class TraitInstance : IBonusProvider
{
    public TraitInstance(TraitModel traitModel, IEnumerable<ProficiencyModel> proficiencyChoices, IEnumerable<LanguageModel> languageOptions, IEnumerable<TraitInstance> subtraitOptions, IEnumerable<SpellModel> spellOptions)
    {
        TraitModel = traitModel;
        ProficiencyChoices = proficiencyChoices.ToList();
        LanguageOptions = languageOptions.ToList();
        SubtraitOptions = subtraitOptions.ToList();
        SpellOptions = spellOptions.ToList();
    }

    public TraitModel TraitModel { get; }

    public List<TraitInstance> SubtraitOptions { get; }

    public List<SpellModel> SpellOptions { get; }

    public List<ProficiencyModel> ProficiencyChoices { get; }

    public List<LanguageModel> LanguageOptions { get; }

    public virtual Task HandleCommand(ICommand command)
    {
        foreach (var subtrait in SubtraitOptions)
        {
            subtrait.HandleCommand(command);
        }

        return Task.CompletedTask;
    }

    public override bool Equals(object? obj)
    {
        return obj is TraitInstance traitInstance
            && traitInstance.TraitModel == TraitModel
            && traitInstance.SubtraitOptions.Equals(SubtraitOptions)
            && traitInstance.SpellOptions.Equals(SpellOptions)
            && traitInstance.ProficiencyChoices.Equals(ProficiencyChoices)
            && traitInstance.LanguageOptions.Equals(LanguageOptions);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(TraitModel, SubtraitOptions, SpellOptions, ProficiencyChoices, LanguageOptions);
    }
}