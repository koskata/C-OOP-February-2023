using FrontDeskApp;

using NUnit.Framework;

using System;

namespace BookigApp.Tests
{
    public class Tests
    {
        Hotel hotel;

        [SetUp]
        public void Setup()
        {
            hotel = new Hotel("Ramada", 5);
        }

        [Test]
        public void ConstructorSetsProperlyDataForHotel()
        {
            Assert.AreEqual("Ramada", hotel.FullName);
            Assert.AreEqual(5, hotel.Category);
            Assert.AreEqual(0, hotel.Turnover);
            Assert.IsNotNull(hotel.Rooms);
            Assert.IsNotNull(hotel.Bookings);
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase(" ")]
        [TestCase("    ")]
        public void FullNameThrowsIfValueIsNullOrWhiteSpace(string name)
        {
            Assert.Throws<ArgumentNullException>(() => new Hotel(name, 5));
        }

        [TestCase(0)]
        [TestCase(6)]
        [TestCase(10)]
        [TestCase(-10)]
        public void CategoryThrowsIfIsLessThan1OrMoreThan5StarsHotel(int stars)
        {
            Assert.Throws<ArgumentException>(() => new Hotel("Ramada", stars));
        }

        [Test]
        public void AddRoomShouldAddOneRoomToList()
        {
            Room room = new Room(5, 100);

            hotel.AddRoom(room);

            Assert.AreEqual(1, hotel.Rooms.Count);
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void BookRoomThrowsIfAdultsAreLessOrEqualTo0(int adults)
        {
            Assert.Throws<ArgumentException>(() => hotel.BookRoom(adults, 10, 7, 150));
        }

        [TestCase(-1)]
        [TestCase(-5)]
        public void BookRoomThrowsIfChildrenAreLessThan0(int children)
        {
            Assert.Throws<ArgumentException>(() => hotel.BookRoom(5, children, 7, 150));
        }


        [TestCase(0)]
        [TestCase(-1)]
        public void BookRoomThrowsIfDuratuionIsLessThan1(int duration)
        {
            Assert.Throws<ArgumentException>(() => hotel.BookRoom(5, 10, duration, 150));
        }

        [Test]
        public void BookRoomWorkCorrect()
        {
            Room room = new Room(5, 100);
            Room room2 = new Room(3, 120);

            hotel.AddRoom(room);
            hotel.AddRoom(room2);

            hotel.BookRoom(2, 2, 7, 1000);

            Assert.AreEqual(1, hotel.Bookings.Count);
            Assert.AreEqual(700, hotel.Turnover);
        }

    }
}