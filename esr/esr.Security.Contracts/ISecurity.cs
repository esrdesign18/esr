using esr.Models.Security;

namespace esr.Security.Contracts
{
    public interface ISecurity
    {
        string GenerateTicket(string userUniqueID, AuthenticationInfo authenticationInfo);
        string ReGenerateTicket(AuthenticationInfo authInfo, string userUniqueID);

        AuthenticationInfo ReadTicket(string ticketValue, string userUniqueID);

        bool IsAllowedForRegularContent(AuthenticationInfo authInfo);
        bool IsAllowedForManagementContent(AuthenticationInfo authInfo);
    }
}
