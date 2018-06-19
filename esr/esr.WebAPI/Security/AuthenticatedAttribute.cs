using esr.Models.Security;
using esr.Security.Contracts;
using esr.WebAPI.Security.Helpers;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;

namespace esr.WebAPI.Security
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    internal class AuthenticatedAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        public bool AllowAnonymous { get; set; }
        public bool RequireManagerAccess { get; set; }

        #region Security Service
        //has to be injected at run time
        public static ISecurity _securityService = null;
        public static ISecurity SecurityService
        {
            get
            {
                return _securityService ?? throw new NullReferenceException("SecurityService was not injected to the Authenticated Attribute");
            }
        }
        #endregion Security Service

        /// <summary>Authenticates the request.</summary>
        /// <param name="context">The authentication context.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A Task that will perform authentication.</returns>
        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            try
            {
                if(!AllowAnonymous)
                {
                    //read the ticket
                    AuthenticationInfo authInfo = context.Request.Headers.GetAuthenticationInfo(SecurityService);

                    //TODO: Check WTE you want using the SecurityService
                    bool canAccess = RequireManagerAccess ?
                        SecurityService.IsAllowedForManagementContent(authInfo) :
                        SecurityService.IsAllowedForRegularContent(authInfo);

                    if (!canAccess)
                        throw new UnauthorizedAccessException("You are not allowed to perform this operation with this ticket");

                    //Regenerating a ticket with the same data - to reset the ticket life span
                    context.Request.Headers.RefreshAuthenticationInfo(SecurityService, authInfo);
                }
            }
            catch (Exception ex)
            {
                context.ErrorResult = new AuthenticationFailureResult(ex.Message, context.Request);
            }

            return Task.FromResult(0);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        /// <summary>pass the AuthenticationInfo value from the request to the response - if present</summary>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            string authValue = actionExecutedContext.Request.Headers.GetAuthenticationInfoValue();
            if (actionExecutedContext.Response != null)
            {
                actionExecutedContext.Response.Headers.AddAuthenticationInfoValue(authValue);
            }
        }
    }
}