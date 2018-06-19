using System;
using System.Configuration;

namespace esr.WebAPI.Proxy
{
    /// <summary>Handle settings from config</summary>
    internal static class SettingsManager
    {
        private static Uri _APIBaseUrl;
        public static Uri APIBaseUrl
        {
            get
            {
                if (_APIBaseUrl == default(Uri))
                {
                    string _APIBaseUrlString = ConfigurationManager.AppSettings["APIBaseUrl"];
                    if (String.IsNullOrWhiteSpace(_APIBaseUrlString))
                        throw new ArgumentNullException("APIBaseUrl", "You have to set APIBaseUrl in your AppSettings sections of the config");
                    _APIBaseUrl = new Uri(_APIBaseUrlString);
                }
                return _APIBaseUrl;
            }
        }
    }
}
