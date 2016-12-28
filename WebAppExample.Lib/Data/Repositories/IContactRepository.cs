using System.Collections.Generic;
using WebAppExample.Lib.Data.Models;

namespace WebAppExample.Lib.Data.Repositories
{
    public interface IContactRepository
    {
        List<Contact> LoadAll();
        Contact LoadById(int id);
        void Add(Contact contact);
        void Update(Contact contact);
        void Delete(int id);
    }
}
