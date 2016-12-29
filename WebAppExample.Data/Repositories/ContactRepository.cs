using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using WebAppExample.Data.DataContext;
using WebAppExample.Lib.Data.Models;
using WebAppExample.Lib.Data.Repositories;
using WebAppExample.Lib.Exceptions;

namespace WebAppExample.Data.Repositories
{
    public class ContactRepository : IContactRepository, IDisposable
    {
        private WebAppExampleDataContext db = new WebAppExampleDataContext();

        public void Add(Contact contact)
        {
            db.Contacts.Add(contact);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            try
            {
                db.Contacts.Remove(db.Contacts.Find(id));
                db.SaveChanges();
            }
            catch (Exception)
            {
                if (ContactNotExist(id))
                    throw new ContactNotFoundException();

                throw;
            }
        }

        public List<Contact> LoadAll()
        {
            return db.Contacts.ToList();
        }

        public Contact LoadById(int id)
        {
            return db.Contacts.Find(id);
        }

        public void Update(Contact contact)
        {
            db.Entry(contact).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (ContactNotExist(contact.Id))
                    throw new ContactNotFoundException();

                throw;
            }
        }

        public void Dispose()
        {
            db.Dispose();
        }

        private bool ContactNotExist(int id)
        {
            return db.Contacts.Count(e => e.Id == id) == 0;
        }
    }
}