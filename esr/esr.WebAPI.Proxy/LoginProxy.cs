using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace esr.WebAPI.Proxy
{
    public class LoginProxy
    {
        private readonly HttpClient httpClient;
        public LoginProxy()
        {
            httpClient = new HttpClient()
            {
                BaseAddress = SettingsManager.APIBaseUrl
            };
        }
        public async Task<string> Login(bool isManager)
        {
            var resp = await httpClient.PostAsJsonAsync("/api/Login", isManager);
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadAsAsync<string>();
        }
    }
}
