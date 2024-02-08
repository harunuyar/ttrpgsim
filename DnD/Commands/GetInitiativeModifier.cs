namespace DnD.Commands;

using DnD.Entities.Characters;
using TableTopRpg.Commands;

internal class GetInitiativeModifier : DndCommand
{
    public GetInitiativeModifier(Character character) : base(character)
    {
    }

    protected override ICommandResult ExecuteDndCommand()
    {
        return SummarizedIntegerResult.Success(this, Character.AttributeSet.Dexterity.GetModifier(), "Dexterity Modifier");
    }
}
