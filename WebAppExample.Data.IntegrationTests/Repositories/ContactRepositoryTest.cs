using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Utils.Resolution;
using WebAppExample.Lib.Data.Models;
using WebAppExample.Lib.Data.Repositories;
using WebAppExample.Lib.Exceptions;

namespace WebAppExample.Data.IntegrationTests.Repositories
{
    [TestFixture]
    public class ContactRepositoryTest
    {
        IContactRepository _contactRepository;

        [SetUp]
        public void Setup()
        {
            IoC.AddModule<WebAppExampleDataDependencyModule>();
            _contactRepository = IoC.Get<IContactRepository>();
        }

        [TearDown]
        public void TearDown()
        {
            _contactRepository.LoadAll().ForEach(x => _contactRepository.Delete(x.Id));
        }

        [Test]
        public void AddMany_LoadAll_DeleteAll()
        {
            var contacts = new List<Contact>();

            contacts.Add(new Contact { Name = "test 1", Email = "test1@test.com" });
            contacts.Add(new Contact { Name = "test 2", Email = "test2@test.com" });
            contacts.Add(new Contact { Name = "test 3", Email = "test3@test.com" });

            contacts.ForEach(_contactRepository.Add);

            var addedContacts = _contactRepository.LoadAll();

            Assert.That(addedContacts, Is.Not.Null);
            Assert.That(addedContacts, Is.Not.Empty);
            Assert.That(addedContacts, Has.Count.EqualTo(3));

            addedContacts.ForEach(x => _contactRepository.Delete(x.Id));

            var removedContacts = _contactRepository.LoadAll();

            Assert.That(removedContacts, Is.Empty);
        }

        [Test]
        public void Add_LoadById_Update_Delete()
        {
            var random = new Random().Next(100);

            var contact = new Contact
            {
                Name = $"test {random}",
                Email = $"test.{random}@test.com",
                BirthDate = new DateTime(1990, 01, 01),
                Address = $"{random} st",
                Phone = $"55123456{random}"
            };

            _contactRepository.Add(contact);

            var id = _contactRepository.LoadAll().Last().Id;

            var addedContact = _contactRepository.LoadById(id);

            Assert.That(addedContact, Is.Not.Null);
            Assert.That(addedContact.Id, Is.EqualTo(id));
            Assert.That(addedContact.Name, Is.EqualTo($"test {random}"));
            Assert.That(addedContact.Email, Is.EqualTo($"test.{random}@test.com"));
            Assert.That(addedContact.BirthDate, Is.EqualTo(new DateTime(1990, 01, 01)));
            Assert.That(addedContact.Address, Is.EqualTo($"{random} st"));
            Assert.That(addedContact.Phone, Is.EqualTo($"55123456{random}"));

            addedContact.Name = $"test {random + 1}";
            addedContact.Email = $"test.{random + 1}@test.com";
            addedContact.BirthDate = new DateTime(2000, 01, 01);
            addedContact.Address = $"{random + 1} st";
            addedContact.Phone = $"55123456{random + 1}";

            _contactRepository.Update(addedContact);

            var updatedContact = _contactRepository.LoadById(id);

            Assert.That(updatedContact, Is.Not.Null);
            Assert.That(updatedContact.Id, Is.EqualTo(id));
            Assert.That(updatedContact.Name, Is.EqualTo($"test {random + 1}"));
            Assert.That(updatedContact.Email, Is.EqualTo($"test.{random + 1}@test.com"));
            Assert.That(updatedContact.BirthDate, Is.EqualTo(new DateTime(2000, 01, 01)));
            Assert.That(updatedContact.Address, Is.EqualTo($"{random + 1} st"));
            Assert.That(updatedContact.Phone, Is.EqualTo($"55123456{random + 1}"));

            _contactRepository.Delete(id);

            var removedContact = _contactRepository.LoadById(id);

            Assert.That(removedContact, Is.Null);
        }

        [Test]
        public void Delete_Not_Existing_Id_Should_Throw_ContactNotFoundException()
        {
            Assert.Throws<ContactNotFoundException>(() => _contactRepository.Delete(1000));
        }

        [Test]
        public void Update_Not_Existing_Contact_Should_Throw_ContactNotFoundException()
        {
            var contact = new Contact
            {
                Id = 1000,
                Name = "test",
                Email = "t@test.com"
            };

            Assert.Throws<ContactNotFoundException>(() => _contactRepository.Update(contact));
        }
    }
}
