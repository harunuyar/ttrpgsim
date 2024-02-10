namespace Dnd.Tests.CommandSystem;

using Dnd.CommandSystem.Commands.BooleanResultCommands;
using Dnd.CommandSystem.Commands.IntegerResultCommands;
using Dnd.Entities.Attributes;
using Dnd.Entities.Characters;
using Dnd.Entities.Classes.Predefined;
using Dnd.Entities.Items;
using Dnd.Entities.Items.Equipments.Armors;
using Dnd.Entities.Items.Equipments.Armors.HeavyArmor;
using Dnd.Entities.Races;
using Dnd.Entities.Skills.Predefined;

[TestClass]
public class CommandSystemTest
{
    private Character? character;

    [TestInitialize]
    public void TestInitialize()
    {
        character = new Character("Cengiz", Race.Human);

        character.AttributeSet.Set(strength: 15, dexterity: 10, constitution: 14, intelligence: 8, wisdom: 12, charisma: 13);

        character.Levels.Add(new Level(Fighter.Instance, 2));
        character.HitPoints.AddHitPointRoll((int)Fighter.Instance.HitDie);
        character.HitPoints.AddHitPointRoll(7);

        character.SetSkillProficiency(Intimidation.Instance, true);
        character.SetSkillProficiency(Athletics.Instance, true);

        character.Inventory.EquipArmor(new Item(new ChainMailArmor()));
        character.Inventory.EquipShield(new Item(new Shield()));
    }

    [TestMethod]
    [DataRow(EAttributeType.Strength, 15)]
    [DataRow(EAttributeType.Dexterity, 10)]
    [DataRow(EAttributeType.Constitution, 14)]
    [DataRow(EAttributeType.Intelligence, 8)]
    [DataRow(EAttributeType.Wisdom, 12)]
    [DataRow(EAttributeType.Charisma, 13)]
    public void TestGetAttributeScore(EAttributeType attribute, int score)
    {
        var getAttributeScoreCommand = new GetAttributeScore(character!, attribute);
        var result = getAttributeScoreCommand.Execute();

        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(score, result.Value);
    }

    [TestMethod]
    public void TestGetMaxHitPoints()
    {
        var getMaxHitPointsCommand = new GetMaxHP(character!);
        var result = getMaxHitPointsCommand.Execute();

        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(21, result.Value); // 10 (first roll) + 7 (second roll) + 2 * 2 (Constitution modifier)
    }

    [TestMethod]
    public void TestGetSkillProficiencyTrue()
    {
        var getSkillProficiencyCommand = new GetSkillProficiency(character!, Intimidation.Instance);
        var result = getSkillProficiencyCommand.Execute();

        Assert.IsTrue(result.IsSuccess);
        Assert.IsTrue(result.Value);
    }

    [TestMethod]
    public void TestGetSkillProficiencyFalse()
    {
        var getSkillProficiencyCommand = new GetSkillProficiency(character!, Perception.Instance);
        var result = getSkillProficiencyCommand.Execute();

        Assert.IsTrue(result.IsSuccess);
        Assert.IsFalse(result.Value);
    }

    [TestMethod]
    public void TestGetSkillModifierWithProficiency()
    {
        var getSkillModifierCommand = new GetSkillModifier(character!, Athletics.Instance);
        var result = getSkillModifierCommand.Execute();

        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(4, result.Value);
    }

    [TestMethod]
    public void TestGetSkillModifierWithoutProficiency()
    {
        var getSkillModifierCommand = new GetSkillModifier(character!, Investigation.Instance);
        var result = getSkillModifierCommand.Execute();

        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(-1, result.Value);
    }

    [TestMethod]
    [DataRow(EArmorType.Light, true)]
    [DataRow(EArmorType.Medium, true)]
    [DataRow(EArmorType.Heavy, true)]
    [DataRow(EArmorType.Shield, true)]
    [DataRow(EArmorType.All, true)]
    public void TestGetArmorProficiency(EArmorType armorType, bool proficiency)
    {
        var getArmorProficiencyCommand = new GetArmorProficiency(character!, armorType);
        var result = getArmorProficiencyCommand.Execute();

        Assert.IsTrue(result.IsSuccess);
        Assert.IsTrue(proficiency);
    }

    [TestMethod]
    public void TestGetArmorClass()
    {
        var getArmorClassCommand = new GetArmorClass(character!);
        var result = getArmorClassCommand.Execute();

        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(18, result.Value); // 16 (Chain Mail) + 2 (Shield)
    }
}