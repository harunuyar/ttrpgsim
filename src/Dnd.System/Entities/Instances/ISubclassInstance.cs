namespace Dnd.System.Entities.Instances;

using Dnd._5eSRD.Models.Subclass;

public interface ISubclassInstance : ICommandHandler
{
    SubclassModel SubclassModel { get; }
    ISpellcastingAbility? Spellcasting { get; }
}
