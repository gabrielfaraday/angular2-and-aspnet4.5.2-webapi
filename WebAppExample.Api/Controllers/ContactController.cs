using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Utils.Resolution;
using WebAppExample.Api.ViewModels;
using WebAppExample.Data;
using WebAppExample.Lib.Exceptions;
using WebAppExample.Lib.Handlers;
using WebAppExample.Lib.Loaders;

namespace WebAppExample.Api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api")]
    [Authorize]
    public class ContactController : ApiController
    {
        ContactLoader _contactLoader;
        ContactHandler _contactHandler;

        public ContactController()
        {
            IoC.AddModule<WebAppExampleDataDependencyModule>();
            _contactLoader = IoC.Get<ContactLoader>();
            _contactHandler = IoC.Get<ContactHandler>();
        }

        [Route("contacts")]
        public HttpResponseMessage GetContacts()
        {
            var contacts = _contactLoader
                .LoadAll()
                .Select(x => x.ToContactViewModel())
                .OrderBy(x => x.Name)
                .ToList();

            return Request.CreateResponse(HttpStatusCode.OK, contacts);
        }

        [Route("contacts/{id}")]
        public HttpResponseMessage GetContact(int id)
        {
            ContactViewModel contact;

            try
            {
                contact = _contactLoader.LoadById(id).ToContactViewModel();
            }
            catch (ContactNotFoundException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error while getting contact");
            }

            return Request.CreateResponse(HttpStatusCode.OK, contact);
        }

        [HttpPut]
        [Route("contacts")]
        public HttpResponseMessage PutContact(ContactViewModel contact)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                _contactHandler.Update(contact.ToContactModel());
            }
            catch (ContactNotFoundException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error while updating contact");
            }

            return Request.CreateResponse(HttpStatusCode.OK, contact);
        }

        [HttpPost]
        [Route("contacts")]
        public HttpResponseMessage PostContact(ContactViewModel contact)
        {
            if (contact == null || !ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                _contactHandler.Add(contact.ToContactModel());
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error while adding new contact");
            }

            return Request.CreateResponse(HttpStatusCode.OK, contact);
        }

        [HttpDelete]
        [Route("contacts")]
        public HttpResponseMessage DeleteContact(int id)
        {
            if (id <= 0)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                _contactHandler.Delete(id);
            }
            catch (ContactNotFoundException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error while deleting contact");
            }

            return Request.CreateResponse(HttpStatusCode.OK, "Contact successfully deleted");
        }
    }
}