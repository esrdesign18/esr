using esr.Models.Security;
using esr.Security.Contracts;

namespace esr.Security.Moq
{
    internal class SecurityMoq : ISecurity
    {
        public string GenerateTicket(string userUniqueID, AuthenticationInfo authenticationInfo)
        {
            return null;
        }

        public string ReGenerateTicket(AuthenticationInfo authInfo, string userUniqueID)
        {
            return null;
        }

        public AuthenticationInfo ReadTicket(string ticketValue, string userUniqueID)
        {
            return null;
        }

        public bool IsAllowedForRegularContent(AuthenticationInfo authInfo)
        {
            return true;
        }

        public bool IsAllowedForManagementContent(AuthenticationInfo authInfo)
        {
            return true;
        }
    }
}
