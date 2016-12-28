using Moq;
using NUnit.Framework;
using WebAppExample.Lib.Data.Models;
using WebAppExample.Lib.Data.Repositories;
using WebAppExample.Lib.Loaders;

namespace WebAppExample.Lib.Tests.Loaders
{
    [TestFixture]
    public class UserLoaderTest
    {
        Mock<IUserRepository> _userRepositoryMock;

        UserLoader _userManager;

        [SetUp]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userManager = new UserLoader(_userRepositoryMock.Object);
        }

        [Test]
        public void CheckUser_Should_Call_Repository_To_Check_If_User_Exists()
        {
            var user = new User
            {
                UserName = "test",
                PasswordHash = "test"
            };

            _userManager.LoadByUserName(user.UserName);

            _userRepositoryMock.Verify(x => x.LoadByUserName(user.UserName), Times.Once());
        }

        [Test]
        public void CheckUser_Should_Return_User_If_Exists()
        {
            var user = new User
            {
                UserName = "test",
                PasswordHash = "t"
            };

            _userRepositoryMock.Setup(x => x.LoadByUserName(user.UserName)).Returns(user);

            var result = _userManager.LoadByUserName(user.UserName);

            Assert.That(result.UserName, Is.EqualTo("test"));
            Assert.That(result.PasswordHash, Is.EqualTo("t"));
        }

        [Test]
        public void CheckUser_Should_Return_Null_If_User_Does_Not_Exist()
        {
            var user = new User
            {
                UserName = "test",
                PasswordHash = "test"
            };

            var result = _userManager.LoadByUserName(user.UserName);

            Assert.That(result, Is.Null);
        }
    }
}
