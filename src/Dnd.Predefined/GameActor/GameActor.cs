﻿namespace Dnd.Predefined.GameActor;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;
using Dnd.System.Entities.Instances;

public class GameActor : IGameActor
{
    public GameActor(string name, IRaceInstance race, ISubraceInstance? subrace, IAlignmentInstance? alignment, LevelInstance firstLevel)
    {
        Name = name;
        Race = race;
        Subrace = subrace;
        Alignment = alignment;
        AttributeSet = new AbilitySet();
        LevelInfo = new LevelInfo(firstLevel);
        HitPoints = new HitPoints();
        HitPoints.AddHitPointRoll(firstLevel.ClassInstance.ClassModel.HitDie ?? 1);
        EffectsTable = new EffectsTable();
        Inventory = new Inventory();
        ActionCounter = new ActionCounter();
        SpellMemory = new SpellMemory();
    }

    public string Name { get; }

    public bool HasInspiration { get; set; }

    public IRaceInstance Race { get; }

    public ISubraceInstance? Subrace { get; }

    public IAlignmentInstance? Alignment { get; }

    public IAbilitySet AttributeSet { get; }

    public ILevelInfo LevelInfo { get; }

    public IHitPoints HitPoints { get; }

    public IEffectsTable EffectsTable { get; }

    public IInventory Inventory { get; }

    public IActionCounter ActionCounter { get; }

    public ISpellMemory SpellMemory { get; }

    public async Task HandleCommand(ICommand command)
    {
        await Race.HandleCommand(command);
        
        if (Subrace != null)
        {
            await Subrace.HandleCommand(command);
        }

        if (Alignment != null)
        {
            await Alignment.HandleCommand(command);
        }

        await ActionCounter.HandleCommand(command);
        await HitPoints.HandleCommand(command);
        await Inventory.HandleCommand(command);
        await SpellMemory.HandleCommand(command);
        await EffectsTable.HandleCommand(command);

        foreach (var c in LevelInfo.GetClasses())
        {
            await c.HandleCommand(command);
        }

        foreach (var level in LevelInfo.GetLevels())
        {
            await level.HandleCommand(command);
        }
    }
}
