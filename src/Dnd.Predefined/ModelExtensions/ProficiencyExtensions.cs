namespace Dnd.Predefined.ModelExtensions;

using Dnd._5eSRD.Models;
using Dnd._5eSRD.Models.EquipmentCategory;
using Dnd._5eSRD.Models.Proficiency;

public static class ProficiencyExtensions
{
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

        if (proficiency.Reference != null)
        {
            var equipmentCategory = await proficiency.Reference.GetModel<EquipmentCategoryModel>();
            if (equipmentCategory?.Equipment == null)
            {
                return false;
            }

            return equipmentCategory.Equipment.Any(e => e.Url == url);
        }

        return false;
    }

    public static async Task<bool> HasProficiency(this ProficiencyModel proficiency, IAPIReference reference)
    {
        if (reference.Url == null)
        {
            return false;
        }

        return await proficiency.HasProficiency(reference.Url);
    }

    public static async Task<bool> HasProficiency(this IAPIReference proficiencyReference, string url)
    {
        var proficiency = await proficiencyReference.GetModel<ProficiencyModel>();
        if (proficiency == null)
        {
            return false;
        }

        return await proficiency.HasProficiency(url);
    }

    public static async Task<bool> HasProficiency(this IAPIReference proficiencyReference, IAPIReference reference)
    {
        if (reference.Url == null)
        {
            return false;
        }

        return await proficiencyReference.HasProficiency(reference.Url);
    }
}
