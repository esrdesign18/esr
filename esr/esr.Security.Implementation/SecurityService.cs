using Aleph1.Logging;
using Aleph1.Security.Contracts;
using esr.Models.Security;
using esr.Security.Contracts;

namespace esr.Security.Implementation
{
    internal class SecurityService : ISecurity
    {
        private readonly ICipher CipherService = null;

        [Logged(LogParameters = false)]
        public SecurityService(ICipher cipherService)
        {
            this.CipherService = cipherService;
        }

        [Logged(LogParameters = false, LogReturnValue = true)]
        public string GenerateTicket(string userUniqueID, AuthenticationInfo authInfo)
        {
            return this.CipherService.Encrypt(SettingsManager.AppPrefix, userUniqueID, authInfo, SettingsManager.TicketDurationTimeSpan);
        }

        [Logged(LogParameters = false, LogReturnValue = true)]
        public string ReGenerateTicket(AuthenticationInfo authInfo, string userUniqueID)
        {
            return this.CipherService.Encrypt(SettingsManager.AppPrefix, userUniqueID, authInfo, SettingsManager.TicketDurationTimeSpan);
        }

        public AuthenticationInfo ReadTicket(string ticketValue, string userUniqueID)
        {
            return this.CipherService.Decrypt<AuthenticationInfo>(SettingsManager.AppPrefix, userUniqueID, ticketValue);
        }

        [Logged(LogParameters = false, LogReturnValue = true)]
        public bool IsAllowedForManagementContent(AuthenticationInfo authInfo)
        {
            return authInfo != default(AuthenticationInfo) && authInfo.IsManager;
        }

        [Logged(LogParameters = false, LogReturnValue = true)]
        public bool IsAllowedForRegularContent(AuthenticationInfo authInfo)
        {
            return authInfo != default(AuthenticationInfo);
        }
    }
}
