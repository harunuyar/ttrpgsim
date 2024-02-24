namespace Dnd.Predefined.Commands.BoolCommands;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.AbilityScore;
using Dnd._5eSRD.Models.Equipment;
using Dnd.Context;
using Dnd.Predefined.Commands.ScoreCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class CanEquipItem : ValueCommand<bool>
{
    public CanEquipItem(IGameActor character, EquipmentModel equipment) : base(character)
    {
        Equipment = equipment;
    }

    public EquipmentModel Equipment { get; }

    protected override async Task InitializeResult()
    {
        var canTakeAnyAction = await new CanTakeAnyAction(Actor).Execute();

        if (!canTakeAnyAction.IsSuccess)
        {
            SetError("CanTakeAnyAction: " + canTakeAnyAction.ErrorMessage);
            return;
        }

        if (!canTakeAnyAction.Value)
        {
            Set(canTakeAnyAction);
            ForceComplete();
            return;
        }

        if (Equipment.StrMinimum.HasValue)
        {
            var str = await DndContext.Instance.GetObject<AbilityScoreModel>(AbilityScores.Str);
            if (str == null)
            {
                SetError("AbilityScoreModel: " + AbilityScores.Str + " not found.");
                return;
            }

            var strScore = await new GetTotalAbilityScore(Actor, str).Execute();

            if (!strScore.IsSuccess)
            {
                SetError("GetTotalAbilityScore: " + strScore.ErrorMessage);
                return;
            }

            if (strScore.Value < Equipment.StrMinimum.Value)
            {
                SetValue(false, $"{Actor.Name} can't equip {Equipment} because they don't meet the strength requirement.");
                return;
            }
        }

        SetValue(true, $"{Actor.Name} can equip {Equipment}.");
    }
}
