using System;

using NUnit.Framework;

namespace Skeleton.Tests
{
    [TestFixture]
    public class DummyTests
    {
        [Test]
        public void DummyLoosesHealthAfterAttack()
        {
            Dummy dummy = new(100, 1000);

            dummy.TakeAttack(10);

            Assert.That(dummy.Health, Is.EqualTo(90), "Health doesn't correct!");
        }

        [Test]
        public void DeadDummyThrowsExceptionWhenDies()
        {
            Dummy dummy = new(0, 1000);

            Assert.Throws<InvalidOperationException>(() => dummy.TakeAttack(5));
        }

        [Test]
        public void DeadDummyCanGiveXP()
        {
            Dummy dummy = new(0, 1000);

            Assert.AreEqual(1000, dummy.GiveExperience());
        }

        [Test]
        public void AliveDummyCantGiveXP()
        {
            Dummy dummy = new(50, 1000);

            Assert.Throws<InvalidOperationException>(() => dummy.GiveExperience());
        }
    }
}