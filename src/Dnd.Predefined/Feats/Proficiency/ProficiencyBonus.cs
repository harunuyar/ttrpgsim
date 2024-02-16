﻿namespace Dnd.Predefined.Feats.Proficiency;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands;

public class ProficiencyBonus : AFeat
{
    private static readonly int[] ProficiencyBonusArr = [2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6];

    public ProficiencyBonus() : base("Proficiency Bonus", "Proficiency bonus based on level")
    {
    }

    public override void HandleCommand(ICommand command)
    {
        if (command is GetProficiencyBonus getProficiencyBonus)
        {
            int level = command.Actor.LevelInfo.Level;

            if (level < 1 || level > 20)
            {
                getProficiencyBonus.SetErrorAndReturn("Level must be between 1 and 20, but it is " + level);
            }
            else
            {
                getProficiencyBonus.SetBaseValue(this, ProficiencyBonusArr[level - 1]);
            }
        }
    }
}
