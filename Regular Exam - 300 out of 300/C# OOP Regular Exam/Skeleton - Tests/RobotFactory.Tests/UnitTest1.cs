using System.Diagnostics;
using System.Reflection;
using System.Text;

using NUnit.Framework;

namespace RobotFactory.Tests
{
    public class Tests
    {
        Factory factory;
        [SetUp]
        public void Setup()
        {
            factory = new Factory("KTM", 100);
        }

        [Test]
        public void FactoryConstructorSetsCorrectData()
        {
            Assert.AreEqual("KTM", factory.Name);
            Assert.AreEqual(100, factory.Capacity);
            Assert.IsNotNull(factory.Robots);
            Assert.IsNotNull(factory.Supplements);
        }

        [Test]
        public void ProduceRobotShouldAddRobotToRobotsList()
        {

            string result = factory.ProduceRobot("2022", 500, 5);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Robot model: 2022 IS: 5, Price: {500:f2}");

            Assert.AreEqual($"Produced --> {sb.ToString().TrimEnd()}", result);

            //Assert.AreEqual($"Produced --> Robot model: 2022 IS: 5, Price: {500:f2}", result); 
            Assert.AreEqual(1, factory.Robots.Count);

        }

        [Test]
        public void ProduceRobotShouldReturnUnableIfCountIsBiggerThanCapacity()
        {
            Factory newFactory = new Factory("Nova", 1);
            newFactory.ProduceRobot("2022", 500, 5);

            Assert.AreEqual($"The factory is unable to produce more robots for this production day!",
                newFactory.ProduceRobot("2023", 400, 7));
        }

        [Test]
        public void ProduceSupplementsShouldAddSupToList()
        {
            string result = factory.ProduceSupplement("Nova", 6);

            Assert.AreEqual("Nova", factory.Supplements[0].Name);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Supplement: Nova IS: 6");

            Assert.AreEqual(sb.ToString().TrimEnd(), result);
        }

        [Test]
        public void UpgradeRobotShouldReturnFalseIfRobotSuppContainsThisSuppOrRobotInterfaceISNotEqualToSuppInterface()
        {
            Robot robot = new Robot("ZTR", 500, 5);
            Supplement supplement = new Supplement("ZTR-67", 5);

            factory.UpgradeRobot(robot, supplement);

            Assert.AreEqual(false, factory.UpgradeRobot(robot, supplement));

            Robot robot2 = new Robot("ZTR", 500, 5);
            Supplement supplement2 = new Supplement("ZTR-98", 6);

            robot2.Supplements.Add(supplement);

            Assert.AreEqual(false, factory.UpgradeRobot(robot, supplement));
        }

        [Test]
        public void UpgradeRobotShouldReturnTrueAndAddSuppToSuppList()
        {
            Robot robot = new Robot("ZTR", 500, 5);
            Supplement supplement = new Supplement("ZTR-67", 5);

            bool result = factory.UpgradeRobot(robot, supplement);

            Assert.AreEqual("ZTR-67", robot.Supplements[0].Name);
            Assert.AreEqual(1, robot.Supplements.Count);
            Assert.AreEqual(true, result);
        }

        [Test]
        public void SellRobotShouldReturnRobot()
        {
            Robot robot = new Robot("ZTR", 500, 5);
            Robot robot2 = new Robot("STR", 700, 8);
            Robot robot3 = new Robot("KTR", 1500, 8);
            Robot robot4 = new Robot("MTR", 200, 8);

            factory.Robots.Add(robot);
            factory.Robots.Add(robot2);

            Robot newRobot = factory.SellRobot(800);

            Assert.AreEqual("STR", newRobot.Model);
        }
    }
}