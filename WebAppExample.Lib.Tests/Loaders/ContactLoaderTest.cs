using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using WebAppExample.Lib.Data.Models;
using WebAppExample.Lib.Data.Repositories;
using WebAppExample.Lib.Loaders;

namespace WebAppExample.Lib.Tests.Loaders
{
    [TestFixture]
    public class ContactLoaderTest
    {
        Mock<IContactRepository> _contactRepositoryMock;

        ContactLoader _contactLoader;

        [SetUp]
        public void Setup()
        {
            _contactRepositoryMock = new Mock<IContactRepository>();
            _contactLoader = new ContactLoader(_contactRepositoryMock.Object);
        }

        [Test]
        public void LoadAll_Should_Call_Repository_To_Get_All_Contacts()
        {
            _contactLoader.LoadAll();

            _contactRepositoryMock.Verify(x => x.LoadAll(), Times.Once());
        }

        [Test]
        public void LoadAll_Should_Return_List_Of_All_Contacts()
        {
            var contacts = new List<Contact>
            {
                new Contact { Name = "test 1", Email = "teste1@test.com" },
                new Contact { Name = "test 2", Email = "teste2@test.com" },
                new Contact { Name = "test 3", Email = "teste3@test.com" }
            };

            _contactRepositoryMock.Setup(x => x.LoadAll()).Returns(contacts);

            var result = _contactLoader.LoadAll();

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
            Assert.That(result, Has.Count.EqualTo(3));
        }

        [Test]
        public void LoadById_Should_Call_Repository_To_Get_All_Contacts()
        {
            _contactLoader.LoadById(1);

            _contactRepositoryMock.Verify(x => x.LoadById(1), Times.Once());
        }

        [Test]
        public void LoadById_Should_Return_Specific_Contact_When_Existent()
        {
            var contact = new Contact
            {
                Id = 1,
                Name = "test 1",
                Email = "teste1@test.com"
            };

            _contactRepositoryMock.Setup(x => x.LoadById(1)).Returns(contact);

            var result = _contactLoader.LoadById(1);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("test 1"));
        }
    }
}
