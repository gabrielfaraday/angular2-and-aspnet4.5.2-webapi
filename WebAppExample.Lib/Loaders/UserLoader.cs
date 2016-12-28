using WebAppExample.Lib.Data.Models;
using WebAppExample.Lib.Data.Repositories;

namespace WebAppExample.Lib.Loaders
{
    public class UserLoader
    {
        IUserRepository _userRepository;

        public UserLoader(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User LoadByUserName(string userName)
        {
            return _userRepository.LoadByUserName(userName);
        }
    }
}
