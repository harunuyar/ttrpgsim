namespace Dnd.Tests.CommandSystem;

using Dnd.Predefined.Characters;
using Dnd.Predefined.Classes;
using Dnd.Predefined.Feats.Classes.Fighter.FightingStyle;
using Dnd.Predefined.Items.Weapons;
using Dnd.Predefined.Levels.Fighter;
using Dnd.Predefined.Races;
using Dnd.Predefined.Skills;
using Dnd.System.CommandSystem.Commands.EventCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands.Modifiers;
using Dnd.System.CommandSystem.Commands.ListCommands;
using Dnd.System.Entities.Actions;
using Dnd.System.Entities.Actions.Impl;
using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Items;
using Dnd.System.Events.EventListener;

[TestClass]
public class GetActionsTest
{
    [TestMethod]
    public void TestGetActions()
    {
        IEventListener eventListener = new NoEventListener();
        Fighter fighter = new Fighter();

        IGameActor character = new CustomCharacter("Test", new HalfElf(EAttributeType.Strength, EAttributeType.Constitution));
        character.AttributeSet.Set(15, 10, 10, 10, 10, 10);

        var addLevel1 = new AddLevel(eventListener, character, new FighterLevel1(fighter, Acrobatics.Instance, Intimidation.Instance, new TwoWeaponFighting())).Execute();
        Assert.IsTrue(addLevel1.IsSuccess);

        var addLevel2 = new AddLevel(eventListener, character, new FighterLevel2(fighter)).Execute();
        Assert.IsTrue(addLevel2.IsSuccess);

        character.Inventory.EquipWeapon(new Item(new Longsword()), mainHand: true);
        character.Inventory.EquipWeapon(new Item(new Longsword()), mainHand: false);

        var actions = new GetActions(character).Execute();
        Assert.IsTrue(actions.IsSuccess);

        Assert.AreEqual(7, actions.Values.Count); // 2 from Mainhand Longsword, 1 from Offhand Longsword, 2 from UnarmedStrikeAbility, 1 from ActionSurge, 1 from SecondWind

        var bonusWeaponAttack = actions.Values.Where(a => a is WeaponAttack weaponAttack && weaponAttack.ActionType == EActionType.BonusAction).First();
        Assert.IsNotNull(bonusWeaponAttack);

        var damageModifier = new GetDamageModifier(character, null, (WeaponAttack)bonusWeaponAttack).Execute();

        Assert.IsTrue(damageModifier.IsSuccess);

        Assert.AreEqual(3, damageModifier.Value); // str modifier (two weapon fighting)
    }
}
