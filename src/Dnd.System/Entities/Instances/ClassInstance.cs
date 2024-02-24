namespace Dnd.System.Entities.Instances;

using Dnd._5eSRD.Models.Class;
using Dnd._5eSRD.Models.Proficiency;

public class ClassInstance
{
    public ClassInstance(ClassModel classModel, IEnumerable<ProficiencyModel> proficiencyOptions)
    {
        ClassModel = classModel;
        ProficiencyOptions = proficiencyOptions.ToList();
    }

    public ClassModel ClassModel { get; }

    public List<ProficiencyModel> ProficiencyOptions { get; }

    public override bool Equals(object? obj)
    {
        return obj is ClassInstance classInstance
            && classInstance.ClassModel == ClassModel
            && classInstance.ProficiencyOptions.Equals(ProficiencyOptions);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(ClassModel, ProficiencyOptions);
    }
}
