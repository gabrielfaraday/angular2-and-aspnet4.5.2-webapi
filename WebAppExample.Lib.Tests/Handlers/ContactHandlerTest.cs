using Moq;
using NUnit.Framework;
using WebAppExample.Lib.Data.Models;
using WebAppExample.Lib.Data.Repositories;
using WebAppExample.Lib.Handlers;

namespace WebAppExample.Lib.Tests.Handlers
{
    [TestFixture]
    public class ContactHandlerTest
    {
        Mock<IContactRepository> _contactRepositoryMock;

        ContactHandler _contactHandler;

        [SetUp]
        public void Setup()
        {
            _contactRepositoryMock = new Mock<IContactRepository>();
            _contactHandler = new ContactHandler(_contactRepositoryMock.Object);
        }

        [Test]
        public void Add_Should_Call_Repository_To_Add_New_Contact()
        {
            var contact = new Contact
            {
                Name = "test 1",
                Email = "test@test.com"
            };

            _contactHandler.Add(contact);

            _contactRepositoryMock.Verify(x => x.Add(contact), Times.Once());
        }

        [Test]
        public void Uptade_Should_Call_Repository_To_Update_Given_Contact()
        {
            var contact = new Contact
            {
                Id = 1,
                Name = "test 2",
                Email = "test2@test.com"
            };

            _contactHandler.Update(contact);

            _contactRepositoryMock.Verify(x => x.Update(contact), Times.Once());
        }

        [Test]
        public void Delete_Should_Call_Repository_To_Delete_Given_Contact()
        {
            _contactHandler.Delete(1);

            _contactRepositoryMock.Verify(x => x.Delete(1), Times.Once());
        }
    }
}
