using esr.Models.Security;
using esr.Security.Contracts;
using esr.WebAPI.Classes;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http.Filters;

namespace esr.WebAPI.Security.Helpers
{
    internal static class HttpHeadersHelper
    {
        /// <summary>Implement a logic to identify user uniquely</summary>
        private static string GetUserUniqueID(this HttpRequest httpRequest) => httpRequest.UserHostAddress;

        public static string GetAuthenticationInfoValue(this HttpRequestHeaders headers)
        {
            return headers.FirstOrDefault(h => h.Key.Equals(SettingsManager.AuthenticationHeaderKey, StringComparison.OrdinalIgnoreCase)).Value?.FirstOrDefault();
        }
        public static string AddAuthenticationInfoValue(this HttpHeaders headers, string value)
        {
            if (headers.Contains(SettingsManager.AuthenticationHeaderKey))
                headers.Remove(SettingsManager.AuthenticationHeaderKey);

            if (!String.IsNullOrWhiteSpace(value))
                headers.Add(SettingsManager.AuthenticationHeaderKey, value);

            return value;
        }


        internal static AuthenticationInfo GetAuthenticationInfo(this HttpRequestHeaders headers, ISecurity securityService)
        {
            return securityService.ReadTicket(headers.GetAuthenticationInfoValue(), HttpContext.Current.Request.GetUserUniqueID());
        }
        internal static string AddAuthenticationInfo(this HttpHeaders headers, ISecurity securityService, AuthenticationInfo authInfo)
        {
            return headers.AddAuthenticationInfoValue(securityService.GenerateTicket(HttpContext.Current.Request.GetUserUniqueID(), authInfo));
        }
        internal static string RefreshAuthenticationInfo(this HttpRequestHeaders headers, ISecurity securityService, AuthenticationInfo authInfo)
        {
            return headers.AddAuthenticationInfoValue(securityService.ReGenerateTicket(authInfo, HttpContext.Current.Request.GetUserUniqueID()));
        }

        internal static T GetHttpParameter<T>(this HttpAuthenticationContext context, params string[] parameterNames)
        {
            NameValueCollection query = HttpUtility.ParseQueryString(context.Request.RequestUri.Query);

            string possibleValue = null;
            foreach (string parameterName in parameterNames)
            {
                possibleValue = query[parameterName]
                   ?? context?.ActionContext?.ControllerContext?.RouteData?.Values[parameterName]?.ToString();

                if (!String.IsNullOrWhiteSpace(possibleValue))
                    break;
            }

            if (!String.IsNullOrWhiteSpace(possibleValue))
            {
                TypeConverter typeConverter = TypeDescriptor.GetConverter(typeof(T));
                return (T)typeConverter.ConvertFromString(possibleValue);
            }

            throw new ArgumentNullException(String.Join(",", parameterNames));
        }
    }
}