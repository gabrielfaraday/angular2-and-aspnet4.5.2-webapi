using WebAppExample.Lib.Data.Models;
using WebAppExample.Lib.Data.Repositories;

namespace WebAppExample.Lib.Handlers
{
    public class ContactHandler
    {
        private IContactRepository _contactRepository;

        public ContactHandler(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public void Add(Contact contact)
        {
            _contactRepository.Add(contact);
        }

        public void Update(Contact contact)
        {
            _contactRepository.Update(contact);
        }

        public void Delete(int id)
        {
            _contactRepository.Delete(id);
        }
    }
}
