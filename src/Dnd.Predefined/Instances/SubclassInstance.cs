namespace Dnd.Predefined.Instances;

using Dnd._5eSRD.Models.Subclass;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Instances;

public class SubclassInstance : ISubclassInstance
{
    public SubclassInstance(SubclassModel subclass, ISpellcastingAbility? spellcasting)
    {
        SubclassModel = subclass;
        Spellcasting = spellcasting;
    }

    public SubclassModel SubclassModel { get; }

    public ISpellcastingAbility? Spellcasting { get; }

    public async Task HandleCommand(ICommand command)
    {
        if (Spellcasting is not null)
        {
            await Spellcasting.HandleCommand(command);
        }
    }
}
