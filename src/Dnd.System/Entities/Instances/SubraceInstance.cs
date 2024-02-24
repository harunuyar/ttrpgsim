namespace Dnd.System.Entities.Instances;

using Dnd._5eSRD.Models.Language;
using Dnd._5eSRD.Models.Subrace;

public class SubraceInstance
{
    public SubraceInstance(SubraceModel subraceModel, IEnumerable<LanguageModel> languageOptions, IEnumerable<TraitInstance> racialTraits)
    {
        SubraceModel = subraceModel;
        LanguageOptions = languageOptions.ToList();
        RacialTraits = racialTraits.ToList();
    }

    public SubraceModel SubraceModel { get; }

    public List<LanguageModel> LanguageOptions { get; }

    public List<TraitInstance> RacialTraits { get; }

    public override bool Equals(object? obj)
    {
        return obj is SubraceInstance subraceInstance
            && subraceInstance.SubraceModel == SubraceModel
            && subraceInstance.LanguageOptions.Equals(LanguageOptions)
            && subraceInstance.RacialTraits.Equals(RacialTraits);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(SubraceModel, LanguageOptions, RacialTraits);
    }
}
