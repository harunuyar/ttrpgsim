namespace Dnd.Predefined.Definitions.Classes;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.Class;
using Dnd._5eSRD.Models.Proficiency;
using Dnd.Context;
using Dnd.Predefined.Instances;

public class Fighter : ClassInstance
{
    public static async Task<Fighter> Create(IEnumerable<ProficiencyModel> proficiencyOptions)
    {
        var classModel = await DndContext.Instance.GetObject<ClassModel>(Classes.Fighter);
        return classModel == null
            ? throw new InvalidOperationException($"{Classes.Fighter} class model is not found")
            : new Fighter(classModel, proficiencyOptions);
    }

    public Fighter(ClassModel classModel, IEnumerable<ProficiencyModel> proficiencyOptions) 
        : base(classModel, proficiencyOptions, null)
    {
    }
}
