namespace Database.Tests
{
    using System;

    using NUnit.Framework;

    [TestFixture]
    public class DatabaseTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TestIfAreExactly16Elements()
        {
            Database database = new Database(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);

            Assert.AreEqual(16, database.Count);
        }

        [Test]
        public void AddOperationShouldntAddMoreThan16Elements()
        {
            Database database = new Database(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);

            Assert.Throws<InvalidOperationException>(() => database.Add(5));
        }

        [Test]
        public void AddOperationFunctionality()
        {
            Database database = new Database(8);

            int[] array = database.Fetch();

            Assert.AreEqual(1, database.Count);
            Assert.AreEqual(8, array[0]);
        }

        [Test]
        public void RemoveOperationShouldThrowIfNoElements()
        {
            Database database = new Database();

            InvalidOperationException exception =
                Assert.Throws<InvalidOperationException>(() => database.Remove());

            Assert.AreEqual(exception.Message, "The collection is empty!");
        }

        [Test]
        public void RemoveOperationFunctionality()
        {
            Database database = new Database(1, 2);

            Assert.AreEqual(2, database.Count);

            database.Remove();

            Assert.AreEqual(1, database.Count);

            int[] array = database.Fetch();

            Assert.AreEqual(1, array[0]);
        }

        [Test]
        public void FetchFunctionality()
        {
            Database database = new Database(1, 2);

            int[] array = database.Fetch();

            Assert.AreEqual(new int[] { 1, 2 }, array);
        }
    }
}
