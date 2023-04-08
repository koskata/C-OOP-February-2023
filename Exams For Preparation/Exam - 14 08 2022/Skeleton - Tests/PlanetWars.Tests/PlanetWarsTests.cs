using NUnit.Framework;

using System;
using System.ComponentModel;

namespace PlanetWars.Tests
{
    public class Tests
    {
        [TestFixture]
        public class PlanetWarsTests
        {
            Weapon weapon;
            Planet planet;

            [SetUp]
            public void SetUp()
            {
                weapon = new Weapon("Ak", 500, 10);
                planet = new Planet("Earth", 1000);
            }

            [Test]
            public void WeaponConstructorSetsCorrectData()
            {
                Assert.AreEqual("Ak", weapon.Name);
                Assert.AreEqual(500, weapon.Price);
                Assert.AreEqual(10, weapon.DestructionLevel);
            }

            [TestCase(-1)]
            [TestCase(-10)]
            public void WeaponPriceThrowsIfValueIsNegative(double price)
            {
                ArgumentException exception = Assert.Throws<ArgumentException>(() => new Weapon("Ak", price, 10));

                Assert.AreEqual("Price cannot be negative.", exception.Message);
            }

            [Test]
            public void DistructionLevelShouldIncreaseIfCallMethodIncreaseDestructionLevel()
            {
                weapon.IncreaseDestructionLevel();

                Assert.AreEqual(11, weapon.DestructionLevel);
            }

            [Test]
            public void IsNuclearShouldReturnTrueIfDLIsBiggerOrEqualTo10()
            {
                Assert.IsTrue(weapon.IsNuclear);
            }



            [Test]
            public void PlanetConstructorSetsCorrectData()
            {
                Assert.AreEqual("Earth", planet.Name);
                Assert.AreEqual(1000, planet.Budget);
                Assert.IsNotNull(planet.Weapons);
            }

            [TestCase("")]
            [TestCase(null)]
            public void PlanetNameShouldThrowsIfIsNullOrEmptySpace(string name)
            {
                ArgumentException exception = Assert.Throws<ArgumentException>(() => new Planet(name, 1000));

                Assert.AreEqual("Invalid planet Name", exception.Message);
            }

            [TestCase(-1)]
            [TestCase(-10)]
            public void PlanetBudgetShouldThrowsIfIsNegative(double price)
            {
                ArgumentException exception = Assert.Throws<ArgumentException>(() => new Planet("Earth", price));

                Assert.AreEqual("Budget cannot drop below Zero!", exception.Message);
            }

            [Test]
            public void MilitaryPowerRatioShouldFindSumOfAllDestructionLevels()
            {
                planet.AddWeapon(weapon);
                planet.AddWeapon(new Weapon("SMG", 200, 4));

                Assert.AreEqual(14, planet.MilitaryPowerRatio);
            }

            [Test]
            public void SpendFundsThrowsIfAmountIsBiggerThanBudget()
            {
                InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => planet.SpendFunds(1001));

                Assert.AreEqual("Not enough funds to finalize the deal.", exception.Message);
            }

            [Test]
            public void SpendFundsWorkCorrect()
            {
                planet.SpendFunds(500);

                Assert.AreEqual(500, planet.Budget);
            }

            [Test]
            public void AddShouldThrowsIfTheresAlreadyWeaponWithThisName()
            {
                planet.AddWeapon(weapon);

                InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => planet.AddWeapon(new Weapon("Ak", 100, 5)));

                Assert.AreEqual($"There is already a Ak weapon.", exception.Message);
            }

            [Test]
            public void AddShouldWorkCorrect()
            {
                planet.AddWeapon(weapon);

                Assert.AreEqual(1, planet.Weapons.Count);
            }

            [Test]
            public void ProfitShouldWork()
            {
                planet.Profit(100);

                Assert.AreEqual(1100, planet.Budget);
            }

            [Test]
            public void RemoveWeaponShouldRemove()
            {
                planet.AddWeapon(weapon);

                planet.RemoveWeapon("Ak");

                Assert.AreEqual(0, planet.Weapons.Count);
            }

            [Test]
            public void UpgradeShouldThrowIfNameIsNotFound()
            {
                planet.AddWeapon(weapon);



                InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => planet.UpgradeWeapon("SMG"));

                Assert.AreEqual($"SMG does not exist in the weapon repository of Earth", exception.Message);
            }

            [Test]
            public void UpgradeShouldWorkCorrect()
            {
                planet.AddWeapon(weapon);

                planet.UpgradeWeapon("Ak");

                Assert.AreEqual(11, weapon.DestructionLevel);
            }

            [Test]
            public void DestructOpponentShouldThrowIfOppnentIsStrongerThanMe()
            {
                planet.AddWeapon(new Weapon("SMG", 400, -1));

                InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => planet.DestructOpponent(new Planet("Uran", 900)));



                Assert.AreEqual($"Uran is too strong to declare war to!", exception.Message);
            }

            [Test]
            public void DestructOpponentShouldReturnCorrectMessage()
            {
                planet.AddWeapon(weapon);

                Assert.AreEqual($"Uran is destructed!", planet.DestructOpponent(new Planet("Uran", 900)));
            }
        }
    }
}
