namespace FightingArena.Tests
{
    using System;

    using NUnit.Framework;

    [TestFixture]
    public class ArenaTests
    {

        [Test]
        public void ArenaListShouldNotBeNull()
        {
            Arena arena = new Arena();
            Assert.IsNotNull(arena.Warriors);
            Assert.AreEqual(0, arena.Count);
        }

        [Test]
        public void EnrollShouldWorkProperly()
        {
            var arena = new Arena();

            arena.Enroll(new Warrior("Pesho", 100, 110));

            foreach (var warrior in arena.Warriors)
            {
                Assert.AreEqual("Pesho", warrior.Name);
                Assert.AreEqual(100, warrior.Damage);
                Assert.AreEqual(110, warrior.HP);
            }

            Assert.AreEqual(1, arena.Count);
        }

        [Test]
        public void EnrollShouldThrowIfNameAlreadyHasBeenAdded()
        {
            Arena arena = new Arena();

            arena.Enroll(new Warrior("Pesho", 100, 110));

            InvalidOperationException exception =
                Assert.Throws<InvalidOperationException>(() => arena.Enroll(new Warrior("Pesho", 90, 100)));

            Assert.AreEqual("Warrior is already enrolled for the fights!", exception.Message);
            Assert.AreEqual(1, arena.Count);
        }

        [Test]
        public void FightShouldFindIfAttackerIsNull()
        {
            var arena = new Arena();

            Warrior defender = new Warrior("Gosho", 60, 110);

            arena.Enroll(defender);

            InvalidOperationException exception =
                Assert.Throws<InvalidOperationException>(() => arena.Fight("Pesho", "Gosho"));

            Assert.AreEqual($"There is no fighter with name Pesho enrolled for the fights!", exception.Message);
        }


        [Test]
        public void FightShouldFindIfDefenderIsNull()
        {
            var arena = new Arena();

            Warrior attacker = new Warrior("Pesho", 60, 110);

            arena.Enroll(attacker);

            InvalidOperationException exception =
                Assert.Throws<InvalidOperationException>(() => arena.Fight("Pesho", "Gosho"));

            Assert.AreEqual($"There is no fighter with name Gosho enrolled for the fights!", exception.Message);
        }

        [Test]
        public void FightShouldWorkProperly()
        {
            var arena = new Arena();

            Warrior attacker = new Warrior("Pesho", 15, 35);
            Warrior defender = new Warrior("Gosho", 15, 45);

            arena.Enroll(attacker);
            arena.Enroll(defender);

            arena.Fight(attacker.Name, defender.Name);

            Assert.AreEqual(30, defender.HP);
            Assert.AreEqual(20, attacker.HP);
        }
    }
}
