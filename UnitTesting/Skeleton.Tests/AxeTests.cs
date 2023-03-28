using System;

using NUnit.Framework;

namespace Skeleton.Tests
{
    [TestFixture]
    public class AxeTests
    {
        [Test]
        public void AxeLoosesDurabilityAfterAttack()
        {
            //Arrange
            Axe axe = new Axe(1, 10);
            Dummy target = new Dummy(15, 100);

            //Act
            axe.Attack(target);

            Assert.AreEqual(9, axe.DurabilityPoints, "Durabillity doesn't change after attack.");
        }

        [Test]
        public void AttackingWithBrokenWeapon()
        {
            Axe axe = new(1, 0);
            Dummy target = new(15, 100);

            Assert.Throws<InvalidOperationException>(() => axe.Attack(target));

            
        }
    }
}