namespace FightingArena.Tests
{
    using System;
    using System.Threading;
    using System.Xml.Linq;

    using NUnit.Framework;

    using static System.Net.Mime.MediaTypeNames;

    [TestFixture]
    public class WarriorTests
    {
        [Test]
        public void ConstructorShouldSetProperlyData()
        {
            Warrior warrior = new Warrior("Pesho", 90, 100);

            Assert.AreEqual("Pesho", warrior.Name);
            Assert.AreEqual(90, warrior.Damage);
            Assert.AreEqual(100, warrior.HP);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("      ")]
        public void NameShouldThrowIfValueIsNullOrWhiteSpace(string name)
        {
            ArgumentException exception =
                Assert.Throws<ArgumentException>(() => new Warrior(name, 90, 100));

            Assert.AreEqual("Name should not be empty or whitespace!", exception.Message);
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void DamageShouldThrowIfValueIsntPositive(int damage)
        {
            ArgumentException exception =
                Assert.Throws<ArgumentException>(() => new Warrior("Pesho", damage, 100));

            Assert.AreEqual("Damage value should be positive!", exception.Message);
        }


        [TestCase(-1)]
        public void HPShouldThrowIfValueIsNegative(int hp)
        {
            ArgumentException exception =
                Assert.Throws<ArgumentException>(() => new Warrior("Pesho", 90, hp));

            Assert.AreEqual("HP should not be negative!", exception.Message);
        }

        [Test]
        public void AttackShouldThrowIfAttackerHPIsSmallerThanMinAttackHP()
        {
            Warrior warrior = new Warrior("Pesho", 90, 100);
            Warrior attacker = new Warrior("Gosho", 70, 20);

            //int minAttackHP = 30;

            InvalidOperationException exception =
                Assert.Throws<InvalidOperationException>(() => attacker.Attack(warrior));

            Assert.AreEqual("Your HP is too low in order to attack other warriors!", exception.Message);
        }

        [Test]
        public void AttackShouldThrowIfDefenderHPIsSmallerThanMinAttackHP()
        {
            Warrior warrior = new Warrior("Pesho", 90, 100);
            Warrior defender = new Warrior("Gosho", 70, 20);

            InvalidOperationException exception =
                Assert.Throws<InvalidOperationException>(() => warrior.Attack(defender));

            Assert.AreEqual("Enemy HP must be greater than 30 in order to attack him!", exception.Message);
        }

        [Test]
        public void AttackShouldThrowIfEnemyIsStronger()
        {
            Warrior warrior = new Warrior("Pesho", 90, 100);
            Warrior defender = new Warrior("Gosho", 101, 120);

            InvalidOperationException exception =
                Assert.Throws<InvalidOperationException>(() => warrior.Attack(defender));
            Assert.AreEqual("You are trying to attack too strong enemy", exception.Message);
        }

        [Test]
        public void AttackShouldSucceed()
        {
            Warrior warrior = new Warrior("Pesho", 35, 100);
            Warrior defender = new Warrior("Gosho", 25, 40);

            warrior.Attack(defender);

            Assert.AreEqual(5, defender.HP);
            Assert.AreEqual(75, warrior.HP);

        }

        [Test]
        public void AttackShouldMakeWarriorHpTo0IfDefenderDamageGreaterThanWarriorHP()
        {
            Warrior attacker = new Warrior("Pesho", 45, 35);
            Warrior defender = new Warrior("Gosho", 15, 35);

            attacker.Attack(defender);

            Assert.AreEqual(0, defender.HP);
            Assert.AreEqual(20, attacker.HP);
        }

    }
}

