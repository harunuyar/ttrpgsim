namespace Dnd.System.Entities.Instances;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.AbilityScore;
using Dnd._5eSRD.Models.Equipment;
using Dnd.Context;
using Dnd.Predefined.Commands.BonusCommands;
using Dnd.Predefined.Commands.ScoreCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.GameManagers.Dice;

public class EquipmentInstance : IEquipmentInstance
{
    public EquipmentInstance(EquipmentModel equipmentModel)
    {
        EquipmentModel = equipmentModel;
    }

    public EquipmentModel EquipmentModel { get; }

    public virtual async Task HandleCommand(ICommand command)
    {
        if (command is GetAdvantageForSkillCheck advantageForSkillCheck)
        {
            if ((EquipmentModel.StealthDisadvantage ?? false) && advantageForSkillCheck.SkillCheck.Skill.Url == Skills.Stealth)
            {
                advantageForSkillCheck.SetValue(EAdvantage.Disadvantage, "Stealth");
            }
        }
        else if (command is GetSpeed speed)
        {
            if (EquipmentModel.StrMinimum.HasValue)
            {
                var ability = await DndContext.Instance.GetObject<AbilityScoreModel>(AbilityScores.Str);

                if (ability is null)
                {
                    speed.SetError("Str ability score not found");
                    return;
                }

                var abilityScore = await new GetTotalAbilityScore(command.Actor, ability).Execute();

                if (!abilityScore.IsSuccess)
                {
                    speed.SetError("GetTotalAbilityScore: " + abilityScore.ErrorMessage);
                    return;
                }

                if (abilityScore.Value < EquipmentModel.StrMinimum)
                {
                    speed.AddBonus(-10, $"{EquipmentModel.Name} Str Minimum");
                    return;
                }
            }
        }
        else if (command is GetArmorClass armorClass)
        {
            if (EquipmentModel.ArmorClass?.Base is not null)
            {
                if (EquipmentModel.ArmorCategory == EArmorCategory.Shield)
                {
                    armorClass.AddBonus(EquipmentModel.ArmorClass.Base.Value, EquipmentModel.Name ?? "Shield");
                }
                else
                {
                    armorClass.SetBaseValue(EquipmentModel.ArmorClass.Base.Value, EquipmentModel.Name ?? "Armor");

                    if ((EquipmentModel.ArmorClass.DexBonus ?? false) && EquipmentModel.ArmorClass.MaxBonus is null)
                    {
                        return; // Dex bonus was already added and don't touch it.
                    }

                    var ability = await DndContext.Instance.GetObject<AbilityScoreModel>(AbilityScores.Dex);

                    if (ability is null)
                    {
                        armorClass.SetError("Dex ability score not found");
                        return;
                    }

                    var abilityScore = await new GetTotalAbilityScore(command.Actor, ability).Execute();

                    if (!abilityScore.IsSuccess)
                    {
                        armorClass.SetError("GetTotalAbilityScore: " + abilityScore.ErrorMessage);
                        return;
                    }

                    if ((EquipmentModel.ArmorClass.DexBonus ?? false) && abilityScore.Value <= (EquipmentModel.ArmorClass.MaxBonus ?? 0))
                    {
                        return; // Dex bonus was already added and it is already less than the max bonus.
                    }

                    if (EquipmentModel.ArmorClass.DexBonus ?? false)
                    {
                        int difference = (EquipmentModel.ArmorClass.MaxBonus ?? 0) - abilityScore.Value;
                        armorClass.AddBonus(difference, "Armor Max Dex Bonus");
                    }
                    else
                    {
                        armorClass.AddBonus(-abilityScore.Value, "Armor No Dex Bonus");
                    }
                }
            }
        }

        return;
    }

    public virtual Task HandleUsageCommand(ICommand command)
    {
        return Task.CompletedTask;
    }

    public override bool Equals(object? obj)
    {
        return obj is EquipmentInstance equipmentInstance
            && equipmentInstance.EquipmentModel == EquipmentModel;
    }

    public override int GetHashCode()
    {
        return EquipmentModel.GetHashCode();
    }
}
