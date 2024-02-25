namespace Dnd.System.Entities.Instances;

using Dnd._5eSRD.Models.Class;
using Dnd._5eSRD.Models.Proficiency;
using Dnd.Predefined.Commands.BoolCommands;
using Dnd.Predefined.ModelExtensions;
using Dnd.System.CommandSystem.Commands;

public class ClassInstance : IClassInstance
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

    public async Task HandleCommand(ICommand command)
    {
        if (command is HasProficiency hasProficiency)
        {
            foreach (var proficiency in ProficiencyOptions)
            {
                if (await proficiency.HasProficiency(hasProficiency.ProficiencyReference))
                {
                    hasProficiency.SetValue(true, proficiency.Name ?? "Proficiency");
                    return;
                }
            }

            if (command.Actor.LevelInfo.MainClass == this)
            {
                foreach (var proficiency in ClassModel.Proficiencies ?? [])
                {
                    if (await proficiency.HasProficiency(hasProficiency.ProficiencyReference))
                    {
                        hasProficiency.SetValue(true, proficiency.Name ?? "Proficiency");
                        return;
                    }
                }
            }
            else
            {
                foreach (var proficiency in ClassModel.MultiClassing?.Proficiencies ?? [])
                {
                    if (await proficiency.HasProficiency(hasProficiency.ProficiencyReference))
                    {
                        hasProficiency.SetValue(true, proficiency.Name ?? "Proficiency");
                        return;
                    }
                }
            }
        }
    }
}
