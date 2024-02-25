namespace Dnd.System.Entities.Instances;

using Dnd._5eSRD.Models.Language;
using Dnd._5eSRD.Models.Subrace;

public interface ISubraceInstance : ICommandHandler
{
    SubraceModel SubraceModel { get; }
    List<LanguageModel> LanguageOptions { get; }
    List<ITraitInstance> RacialTraits { get; }
}
