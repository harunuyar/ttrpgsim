﻿namespace DnD.CommandSystem.Commands;

using DnD.CommandSystem.Results;
using DnD.Entities.Characters;

internal abstract class DndScoreCommand : DndCommand
{
    public DndScoreCommand(Character character) : base(character)
    {
        Result = IntegerResultWithBonus.Empty(this);
        ShouldCollectBonuses = true;
    }

    public IntegerResultWithBonus Result { get; }

    protected bool ShouldCollectBonuses { get; set; }

    public override IntegerResultWithBonus Execute()
    {
        InitializeResult();

        if (ShouldCollectBonuses && Result.IsSuccess)
        {
            CollectBonuses();
        }

        return Result;
    }

    public abstract void InitializeResult();
}
