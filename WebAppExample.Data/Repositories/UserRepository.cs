using System;
using System.Linq;
using WebAppExample.Data.DataContext;
using WebAppExample.Lib.Data.Models;
using WebAppExample.Lib.Data.Repositories;

namespace WebAppExample.Data.Repositories
{
    public class UserRepository : IUserRepository, IDisposable
    {
        private WebAppExampleDataContext db = new WebAppExampleDataContext();

        public User LoadByUserName(string userName)
        {
            return db.Users.SingleOrDefault(u => u.UserName == userName);
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
