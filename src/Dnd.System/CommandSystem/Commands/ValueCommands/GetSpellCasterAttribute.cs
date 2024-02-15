namespace Dnd.System.CommandSystem.Commands.ValueCommands;

using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.GameActors;

public class GetSpellCasterAttribute : DndValueCommand<EAttributeType>
{
    public GetSpellCasterAttribute(IGameActor character) : base(character)
    {
    }

    protected override void InitializeResult()
    {
        Result.Value = EAttributeType.None;
    }
}
