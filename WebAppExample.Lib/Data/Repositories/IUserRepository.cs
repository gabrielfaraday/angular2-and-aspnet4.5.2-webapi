using WebAppExample.Lib.Data.Models;

namespace WebAppExample.Lib.Data.Repositories
{
    public interface IUserRepository
    {
        User LoadByUserName(string userName);
    }
}
