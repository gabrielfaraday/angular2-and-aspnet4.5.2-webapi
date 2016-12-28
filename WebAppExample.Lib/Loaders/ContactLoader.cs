using System.Collections.Generic;
using WebAppExample.Lib.Data.Models;
using WebAppExample.Lib.Data.Repositories;

namespace WebAppExample.Lib.Loaders
{
    public class ContactLoader
    {
        private IContactRepository _contactRepository;

        public ContactLoader(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public List<Contact> LoadAll()
        {
            return _contactRepository.LoadAll();
        }

        public Contact LoadById(int id)
        {
            return _contactRepository.LoadById(id);
        }
    }
}
