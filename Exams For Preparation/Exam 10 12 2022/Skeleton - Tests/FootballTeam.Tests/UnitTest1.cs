using System;
using System.Numerics;
using System.Xml.Linq;

using NUnit.Framework;

namespace FootballTeam.Tests
{
    public class Tests
    {
        FootballPlayer fp;
        FootballTeam ft;

        [SetUp]
        public void Setup()
        {
            fp = new FootballPlayer("Pesho", 7, "Forward");
            ft = new FootballTeam("Dunav", 22);
        }

        [Test]
        public void ConstructorSetsProperlyFootballPlayer()
        {
            Assert.AreEqual("Pesho", fp.Name);
            Assert.AreEqual(7, fp.PlayerNumber);
            Assert.AreEqual("Forward", fp.Position);
            Assert.AreEqual(0, fp.ScoredGoals);
        }

        [TestCase(null)]
        [TestCase("")]
        public void NameThrowsIfIsNullOrEmpty(string name)
        {
            ArgumentException exception =
                Assert.Throws<ArgumentException>(() => fp = new FootballPlayer(name, 7, "Forward"));

            Assert.AreEqual("Name cannot be null or empty!", exception.Message);
        }

        [TestCase(0)]
        [TestCase(-10)]
        [TestCase(27)]
        [TestCase(99)]
        public void NumberThrowsIfIsSmallerThan1AndBiggerThan21(int number)
        {
            ArgumentException exception =
                Assert.Throws<ArgumentException>(() => fp = new FootballPlayer("Pesho", number, "Forward"));

            Assert.AreEqual("Player number must be in range [1,21]", exception.Message);
        }

        [Test]
        public void PostionThrowsIfIsNotGKOrCMOrFR()
        {
            ArgumentException exception =
                Assert.Throws<ArgumentException>(() => fp = new FootballPlayer("Pesho", 7, "Center Back"));

            Assert.AreEqual("Invalid Position", exception.Message);
        }

        [Test]
        public void ScoreShouldIncreaseScoredGoals()
        {
            fp.Score();

            Assert.AreEqual(1, fp.ScoredGoals);
        }

        [Test]
        public void TeamConstructorShouldSetsCorrectData()
        {
            Assert.AreEqual("Dunav", ft.Name);
            Assert.AreEqual(22, ft.Capacity);

            Assert.IsNotNull(ft.Players);

        }

        [TestCase(null)]
        [TestCase("")]
        public void TeamNameThrowsIfIsNullOrEmpty(string name)
        {
            ArgumentException exception =
                Assert.Throws<ArgumentException>(() => ft = new FootballTeam(name, 22));

            Assert.AreEqual("Name cannot be null or empty!", exception.Message);
        }

        [TestCase(14)]
        [TestCase(1)]
        public void TeamCapacityThrowsIfIsLessThan15(int capacity)
        {
            ArgumentException exception =
                Assert.Throws<ArgumentException>(() => ft = new FootballTeam("Dunav", capacity));

            Assert.AreEqual("Capacity min value = 15", exception.Message);
        }

        [Test]
        public void AddNewShouldWorkCorrect()
        {

            string result = ft.AddNewPlayer(fp);

            Assert.AreEqual(1, ft.Players.Count);
            Assert.AreEqual("Pesho", ft.Players[0].Name);
            Assert.AreEqual($"Added player {fp.Name} in position {fp.Position} with number {fp.PlayerNumber}", result);
        }
        [Test]
        public void AddNewShouldReturnIfCountIsBiggerOrEqualToCapacity()
        {
            for (int i = 0; i < 22; i++)
            {
                ft.AddNewPlayer(fp);
            }

            string result = ft.AddNewPlayer(fp);

            Assert.AreEqual("No more positions available!", result);
        }

        [Test]
        public void PickPlayerShouldWorkFine()
        {
            ft.AddNewPlayer(fp);

            FootballPlayer player = ft.PickPlayer("Pesho");

            Assert.AreEqual(7, player.PlayerNumber);
        }

        [Test]
        public void PlayerScoreShouldIncreaseScoredGoalsAndReturnCorrectMessage()
        {
            ft.AddNewPlayer(fp);

            string result = ft.PlayerScore(7);

            Assert.AreEqual(1, fp.ScoredGoals);
            Assert.AreEqual($"Pesho scored and now has 1 for this season!", result);
        }
    }
}