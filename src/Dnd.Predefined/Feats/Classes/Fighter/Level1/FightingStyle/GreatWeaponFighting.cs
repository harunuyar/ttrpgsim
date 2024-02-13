﻿namespace Dnd.Predefined.Feats.Classes.Fighter.Level1.FightingStyle;

using Dnd.System.CommandSystem.Commands;

public class GreatWeaponFighting : AFeat, IFightingStyle
{
    public GreatWeaponFighting() : base("Great Weapon Fighting", "When you roll a 1 or 2 on a damage die for an attack you make with a melee weapon that you are wielding with two hands, you can reroll the die and must use the new roll.")
    {
    }

    public override void HandleCommand(ICommand command)
    {
        // TODO: check if command is MakeAttackRoll and reroll if 1 or 2
    }
}