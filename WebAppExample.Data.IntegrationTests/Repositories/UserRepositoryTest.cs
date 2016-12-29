using NUnit.Framework;
using Utils.Resolution;
using WebAppExample.Lib.Data.Models;
using WebAppExample.Lib.Data.Repositories;

namespace WebAppExample.Data.IntegrationTests.Repositories
{
    [TestFixture]
    public class UserRepositoryTest
    {
        IUserRepository _userRepository;

        [SetUp]
        public void Setup()
        {
            IoC.AddModule<WebAppExampleDataDependencyModule>();
            _userRepository = IoC.Get<IUserRepository>();
        }

        [Test]
        public void UserExists_Called_To_Existing_User_Should_Return_User()
        {
            var user = new User
            {
                UserName = "admin"
            };

            var result = _userRepository.LoadByUserName(user.UserName);

            Assert.That(result.UserName, Is.EqualTo("admin"));
        }

        [Test]
        public void UserExists_Called_To_Not_Existing_User_Should_Return_Null()
        {
            var user = new User
            {
                UserName = "asdhgashdg",
                PasswordHash = "adm"
            };

            var result = _userRepository.LoadByUserName(user.UserName);

            Assert.That(result, Is.Null);
        }
    }
}

