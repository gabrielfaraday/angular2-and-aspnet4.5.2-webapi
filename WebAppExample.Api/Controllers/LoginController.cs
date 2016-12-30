using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Utils.Resolution;
using WebAppExample.Api.Helpers;
using WebAppExample.Api.ViewModels;
using WebAppExample.Data;
using WebAppExample.Lib.Data.Models;
using WebAppExample.Lib.Loaders;

namespace WebAppExample.Api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api")]
    [AllowAnonymous]
    public class LoginController : ApiController
    {
        UserLoader _userLoader;

        public LoginController()
        {
            IoC.AddModule<WebAppExampleDataDependencyModule>();
            _userLoader = IoC.Get<UserLoader>();
        }

        [HttpPost]
        [Route("login")]
        public HttpResponseMessage Login(UserViewModel user)
        {
            if (user == null || !ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            var existingUser = _userLoader.LoadByUserName(user.UserName);

            if (IsInvalidUserNameOrPassword(user, existingUser))
                return Request.CreateResponse(HttpStatusCode.NotFound);

            var token = EncryptionHelper.CreateToken(_userLoader.LoadByUserName(user.UserName));

            return Request.CreateResponse(HttpStatusCode.OK,
                new
                {
                    username = user.UserName,
                    token,
                    success = true
                });
        }

        private bool IsInvalidUserNameOrPassword(UserViewModel user, User existingUser)
        {
            return existingUser != null
                ? !EncryptionHelper.EncryptPassword(user.Password).Equals(existingUser.PasswordHash)
                : true;
        }
    }
}

