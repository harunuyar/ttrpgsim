namespace Dnd.System.Entities.Instances;

using Dnd._5eSRD.Models.Class;
using Dnd._5eSRD.Models.Proficiency;

public interface IClassInstance : ICommandHandler
{
    ClassModel ClassModel { get; }
    List<ProficiencyModel> ProficiencyOptions { get; }
    ISpellcastingAbility? Spellcasting { get; }
}
