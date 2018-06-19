using Aleph1.Logging;
using esr.Models.Security;
using esr.Security.Contracts;
using esr.WebAPI.Security;
using esr.WebAPI.Security.Helpers;
using Aleph1.WebAPI.ExceptionHandler;
using System.Web;
using System.Web.Http;

namespace esr.WebAPI.Controllers
{
    /// <summary>handle login</summary>
    public class LoginController : ApiController
    {
        private readonly ISecurity SecurityService;

        /// <summary>Initializes a new instance of the <see cref="LoginController"/> class.</summary>
        [Logged(LogParameters = false)]
        public LoginController(ISecurity securityService)
        {
            this.SecurityService = securityService;
        }

        /// <summary>Logins to the app (specify if as manager).</summary>
        /// <param name="isManager">if set to <c>true</c> [is manager].</param>
        [Authenticated(AllowAnonymous = true), Logged, HttpPost, Route("api/Login"), FriendlyMessage("התרחשה שגיאה בעת ההתחברות")]
        public string Login([FromBody] bool isManager)
        {
            return Request.Headers.AddAuthenticationInfo(SecurityService, new AuthenticationInfo()
            {
                IsManager = isManager
            });
        }
    }
}
