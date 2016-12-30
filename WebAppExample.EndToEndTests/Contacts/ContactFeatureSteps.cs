using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using WebAppExample.Api.Controllers;
using WebAppExample.Api.ViewModels;
using WebAppExample.Data.Repositories;

namespace WebAppExample.EndToEndTests.Contacts
{
    [Binding]
    public class ContactFeatureSteps
    {
        ContactViewModel _contact;
        ContactController _contactController;
        HttpResponseMessage _response;

        [AfterScenario]
        public void TearDown()
        {
            var repository = new ContactRepository();
            var contacts = repository.LoadAll();
            contacts.ForEach(x => repository.Delete(x.Id));
        }

        [Given(@"I want to add a new contact")]
        public void GivenIWantToAddANewContact(Table table)
        {
            _contact = table.CreateInstance<ContactViewModel>();
        }

        [Given(@"I have a contact")]
        public void GivenIHaveAContact(Table table)
        {
            _contact = table.CreateInstance<ContactViewModel>();
            WhenICallThePostMethod();
        }

        [Given(@"I edit its name to '(.*)'")]
        public void GivenIEditItsNameTo(string name)
        {
            _contact.Name = name;
        }

        [Given(@"email to '(.*)'")]
        public void GivenEmailTo(string email)
        {
            _contact.Email = email;
        }

        [Given(@"phone to empty")]
        public void GivenPhoneToEmpty()
        {
            _contact.Phone = string.Empty;
        }

        [Given(@"I have following contacts")]
        public void GivenIHaveFollowingContacts(Table table)
        {
            table.CreateSet<ContactViewModel>().ToList().ForEach(p =>
            {
                _contact = p;
                WhenICallThePostMethod();
            });
        }

        [When(@"I call the post method")]
        public void WhenICallThePostMethod()
        {
            InitializeController();
            _response = _contactController.PostContact(_contact);
        }

        [When(@"I call the put method")]
        public void WhenICallThePutMethod()
        {
            InitializeController();

            var repository = new ContactRepository();
            _contact.Id = repository.LoadAll().Last().Id;

            _response = _contactController.PutContact(_contact);
        }

        [When(@"I call the delete method for that contact id")]
        public void WhenICallTheDeleteMethodForThatContactId()
        {
            var repository = new ContactRepository();
            var lastId = repository.LoadAll().Last().Id;

            InitializeController();
            _response = _contactController.DeleteContact(lastId);
        }

        [When(@"I call the get method for for that contact id")]
        public void WhenICallTheGetMethodForForThatContactId()
        {
            var repository = new ContactRepository();
            var lastId = repository.LoadAll().Last().Id;

            InitializeController();
            _response = _contactController.GetContact(lastId);
        }

        [When(@"I call the get method")]
        public void WhenICallTheGetMethod()
        {
            InitializeController();
            _response = _contactController.GetContacts();
        }

        [Then(@"the result should be a reponse message with status '(.*)' and content with contact")]
        public void ThenTheResultShouldBeAReponseMessageWithStatusAndContentWithContact(HttpStatusCode status, Table table)
        {
            _contact = table.CreateInstance<ContactViewModel>();

            ContactViewModel returnedContact;

            ThenTheResultShouldBeAReponseMessageWithStatus(status);

            Assert.IsTrue(_response.TryGetContentValue(out returnedContact));
            PerformAssertionsOnContact(returnedContact, _contact);
        }

        [Then(@"the result should be a reponse message with status '(.*)'")]
        public void ThenTheResultShouldBeAReponseMessageWithStatus(HttpStatusCode status)
        {
            Assert.That(_response.StatusCode, Is.EqualTo(status));
        }

        [Then(@"with content '(.*)'")]
        public void ThenWithContent(string content)
        {
            string message;

            Assert.IsTrue(_response.TryGetContentValue(out message));
            Assert.That(message, Is.EqualTo(content));
        }

        [Then(@"the result should be a reponse message with status '(.*)' and content with contacts")]
        public void ThenTheResultShouldBeAReponseMessageWithStatusAndContentWithContacts(HttpStatusCode status, Table table)
        {
            ThenTheResultShouldBeAReponseMessageWithStatus(status);

            List<ContactViewModel> returnedContacts;

            Assert.IsTrue(_response.TryGetContentValue(out returnedContacts));

            table.CreateSet<ContactViewModel>().ToList().ForEach(p =>
            {
                var contact = returnedContacts.SingleOrDefault(x => x.Name == p.Name);

                Assert.NotNull(contact);
                PerformAssertionsOnContact(contact, p);
            });
        }

        private void InitializeController()
        {
            _contactController = new ContactController();
            _contactController.Request = new HttpRequestMessage();
            _contactController.Configuration = new HttpConfiguration();
        }

        private void PerformAssertionsOnContact(ContactViewModel returnedContact, ContactViewModel expectedContact)
        {
            Assert.That(returnedContact.Name, Is.EqualTo(expectedContact.Name));
            Assert.That(returnedContact.Email, Is.EqualTo(expectedContact.Email));

            if (!string.IsNullOrEmpty(expectedContact.BirthDate))
                Assert.That(returnedContact.BirthDate, Is.EqualTo(expectedContact.BirthDate));
            else
                Assert.That(returnedContact.BirthDate, Is.Null);

            Assert.That(returnedContact.Address, Is.EqualTo(expectedContact.Address));
            Assert.That(returnedContact.Phone, Is.EqualTo(expectedContact.Phone));
        }
    }
}
