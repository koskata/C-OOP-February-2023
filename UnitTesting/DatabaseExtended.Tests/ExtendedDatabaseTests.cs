namespace DatabaseExtended.Tests
{
    using System;

    using ExtendedDatabase;

    using NUnit.Framework;

    [TestFixture]
    public class ExtendedDatabaseTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void AddOperationThrowsIfMaximumLength()
        {
            Person[] people = CreateArray();

            Database db = new Database(people);

            Assert.Throws<InvalidOperationException>(() => db.Add(new Person(17, "Hakan")));
        }

        [Test]
        public void AddFunctionality()
        {
            Database db = new Database(new Person(1, "Pesho"));

            Person result = db.FindByUsername("Pesho");

            Assert.AreEqual(1, db.Count);
            Assert.AreEqual("Pesho", result.UserName);
            Assert.AreEqual(1, result.Id);
        }

        [Test]
        public void AddShouldThrowIfNameExist()
        {
            Database db = new Database(new Person(1, "Pesho"));

            Assert.Throws<InvalidOperationException>(() => db.Add(new Person(2, "Pesho")));
        }

        [Test]
        public void AddShouldThrowIfIdExist()
        {
            Database db = new Database(new Person(1, "Pesho"));

            Assert.Throws<InvalidOperationException>(() => db.Add(new Person(1, "Martin")));
        }

        [Test]
        public void RemoveShouldThrowIfNoElements()
        {
            Database database = new Database();

            Assert.Throws<InvalidOperationException>(() => database.Remove());

        }

        [Test]
        public void RemoveOperationFunctionality()
        {
            Database database = new Database(new Person(1, "Pesho"));

            Assert.AreEqual(1, database.Count);

            database.Remove();

            Assert.AreEqual(0, database.Count);
        }

        [Test]
        public void FindByUserNameShouldThrowIfIsNullOrEmpty()
        {
            Database db = new();

            Assert.Throws<ArgumentNullException>(() => db.FindByUsername(null));

            Assert.Throws<ArgumentNullException>(() => db.FindByUsername(string.Empty));
        }

        [Test]
        public void FindByUserNameShouldThrowIfUsernameDoesNotExist()
        {
            Database db = new(new Person(1, "Pesho"));

            Assert.Throws<InvalidOperationException>(() => db.FindByUsername("Martin"));
        }

        [Test]
        public void FindByUsernameFunctionality()
        {
            Database db = new(new Person(1, "Pesho"));

            Person person = db.FindByUsername("Pesho");

            Assert.AreEqual("Pesho", person.UserName);
        }



        [Test]
        public void FindByIdShouldThrowIfIsNegative()
        {
            Database db = new();

            Assert.Throws<ArgumentOutOfRangeException>(() => db.FindById(-1));
        }

        [Test]
        public void FindByidShouldThrowIfIdDoesNotExist()
        {
            Database db = new(new Person(1, "Pesho"));

            Assert.Throws<InvalidOperationException>(() => db.FindById(999));
        }

        [Test]
        public void FindByIdFunctionality()
        {
            Database db = new(new Person(1, "Pesho"));

            Person person = db.FindById(1);

            Assert.AreEqual(1, person.Id);
        }





        private Person[] CreateArray()
        {
            Person[] people = new Person[16];

            for (int i = 0; i < people.Length; i++)
            {
                people[i] = new Person(i, i.ToString());
            }

            return people;
        }
    }
}