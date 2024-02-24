namespace Dnd.Predefined.Commands.ValueCommands;

using Dnd._5eSRD.Models.AbilityScore;
using Dnd._5eSRD.Models.Class;
using Dnd.Context;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class GetSpellCasterAttribute : ValueCommand<AbilityScoreModel>
{
    public GetSpellCasterAttribute(IGameActor character, ClassModel classModel) : base(character)
    {
        ClassModel = classModel;
    }

    public ClassModel ClassModel { get; }

    protected override async Task InitializeResult()
    {
        var spellCasterAttributeUrl = ClassModel.Spellcasting?.SpellcastingAbility?.Url;

        if (spellCasterAttributeUrl is null)
        {
            return;
        }

        var abilityScore = await DndContext.Instance.GetObject<AbilityScoreModel>(spellCasterAttributeUrl);

        if (abilityScore is null)
        {
            SetError("Ability score not found: " + spellCasterAttributeUrl);
            return;
        }

        SetValue(abilityScore, "Spell caster attribute");
    }
}
