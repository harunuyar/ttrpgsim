namespace Dnd.System.Entities;

using Dnd._5eSRD.Models.Common;
using Dnd._5eSRD.Models.EquipmentCategory;
using Dnd._5eSRD.Models.Proficiency;
using Dnd.Context;
using Dnd.System.Entities.GameActor;

public static class ModelExtensions
{
    public static async Task<bool> HasProficiency(this IGameActor gameActor, string proficiencyUrl)
    {
        List<ProficiencyModel> allProficiencies = [];
        allProficiencies.AddRange(gameActor.Race.StartingProficiencyOptions);
        allProficiencies.AddRange(gameActor.LevelInfo.MainClass?.ProficiencyOptions ?? []);
        allProficiencies.AddRange(gameActor.LevelInfo.MultiClasses.SelectMany(c => c.ProficiencyOptions));
        
        foreach (var p in allProficiencies)
        {
            if (await p.HasProficiency(proficiencyUrl))
            {
                return true;
            }
        }

        List<APIReference> references =
            (gameActor.Race.RaceModel.StartingProficiencies ?? [])
            .Concat(gameActor.Subrace?.SubraceModel?.StartingProficiencies ?? [])
            .Concat(gameActor.LevelInfo.MainClass?.ClassModel.Proficiencies ?? [])
            .Concat(gameActor.LevelInfo.MainClass?.ClassModel.Proficiencies ?? [])
            .Concat(gameActor.LevelInfo.MultiClasses.SelectMany(c => c.ClassModel.MultiClassing?.Proficiencies ?? []))
            .ToList();

        foreach (var reference in references)
        {
            if (reference.Url == null)
            {
                continue;
            }

            var proficiency = await DndContext.Instance.GetObject<ProficiencyModel>(reference.Url);
            if (proficiency != null && await proficiency.HasProficiency(proficiencyUrl))
            {
                return true;
            }
        }

        return false;
    }

    public static async Task<bool> HasProficiency(this ProficiencyModel proficiency, string url)
    {
        if (proficiency.Reference?.Url == url)
        {
            return true;
        }

        if (proficiency?.Reference?.Url == null)
        {
            return false;
        }

        var equipmentCategory = await DndContext.Instance.GetObject<EquipmentCategoryModel>(proficiency?.Reference?.Url!);
        if (equipmentCategory?.Equipment == null)
        {
            return false;
        }

        return equipmentCategory.Equipment.Any(e => e.Url == url);
    }
}
