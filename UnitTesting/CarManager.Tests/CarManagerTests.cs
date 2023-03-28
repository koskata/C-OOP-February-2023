namespace CarManager.Tests
{
    using System;
    using System.Runtime.ConstrainedExecution;

    using NUnit.Framework;

    [TestFixture]
    public class CarManagerTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TestConstructorsIfTheyWorkCorrect()
        {
            Car car = new Car("Toyota", "CHR", 3.6, 50);

            Assert.AreEqual(0, car.FuelAmount);
            Assert.AreEqual("Toyota", car.Make);
            Assert.AreEqual("CHR", car.Model);
            Assert.AreEqual(3.6, car.FuelConsumption);
            Assert.AreEqual(50, car.FuelCapacity);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void MakeSetterShouldThrowIfIsNullOrEmpty(string make)
        {
            Assert.Throws<ArgumentException>(() => new Car(make, "CHR", 3.6, 50));
        }


        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void ModelSetterShouldThrowIfIsNullOrEmpty(string model)
        {
            Assert.Throws<ArgumentException>(() => new Car("Toyota", model, 3.6, 50));
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void FuelConsumptionShouldThrowIfIsSmallerOrEqualTo0(double fuelConsumption)
        {
            Assert.Throws<ArgumentException>(() => new Car("Toyota", "CHR", fuelConsumption, 50));
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void FuelCapacityShouldThrowIfIsNegativeOrEqualTo0(double fuelCapacity)
        {

            Assert.Throws<ArgumentException>(() => new Car("Toyota", "CHR", 3.6, fuelCapacity));
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void RefuelShouldThrowIfIsNegativeOrEqualTo0Fuel(double refuel)
        {
            Car car = new Car("Toyota", "CHR", 3.6, 50);

            Assert.Throws<ArgumentException>(() => car.Refuel(refuel));
        }

        [Test]
        public void RefuelShouldBeEqualToFuelCapacity()
        {
            Car car = new Car("Toyota", "CHR", 3.6, 50);

            car.Refuel(54);

            Assert.AreEqual(50, car.FuelAmount);
        }


        [Test]
        public void DriveThrowIfFuelNeededIsBiggerThanFuelAmount()
        {
            Car car = new Car("Toyota", "CHR", 3.6, 50);

            Assert.Throws<InvalidOperationException>(() => car.Drive(10));
        }

        [Test]
        public void DriveMethodFunctionality()
        {
            Car car = new Car("Toyota", "CHR", 3.6, 50);

            car.Refuel(10);

            car.Drive(100);

            Assert.AreEqual(6.4, car.FuelAmount);
        }


    }
}