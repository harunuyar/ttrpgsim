namespace Dnd.System.Entities.Instances;

using Dnd._5eSRD.Models.Language;
using Dnd._5eSRD.Models.Proficiency;
using Dnd._5eSRD.Models.Spell;
using Dnd._5eSRD.Models.Trait;

public interface ITraitInstance : ICommandHandler
{
    TraitModel TraitModel { get; }
    List<ITraitInstance> SubtraitOptions { get; }
    List<SpellModel> SpellOptions { get; }
    List<ProficiencyModel> ProficiencyChoices { get; }
    List<LanguageModel> LanguageOptions { get; }
}
