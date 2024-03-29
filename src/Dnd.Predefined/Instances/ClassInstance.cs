﻿namespace Dnd.Predefined.Instances;

using Dnd._5eSRD.Models.Class;
using Dnd._5eSRD.Models.Proficiency;
using Dnd.Predefined.Commands.BoolCommands;
using Dnd.Predefined.ModelExtensions;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Instances;

public class ClassInstance : IClassInstance
{
    public ClassInstance(ClassModel classModel, IEnumerable<ProficiencyModel> proficiencyOptions, ISpellcastingAbility? spellcasting)
    {
        ClassModel = classModel;
        ProficiencyOptions = proficiencyOptions.ToList();
        Spellcasting = spellcasting;
    }

    public ClassModel ClassModel { get; }

    public List<ProficiencyModel> ProficiencyOptions { get; }

    public ISpellcastingAbility? Spellcasting { get; }

    public async Task HandleCommand(ICommand command)
    {
        if (Spellcasting is not null)
        {
            await Spellcasting.HandleCommand(command);
        }

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
