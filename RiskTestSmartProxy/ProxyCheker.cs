using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RiskTestSmartProxy
{
    public static class ProxyCheker
    {

        private static async Task<string> RequestToIp(HttpClientHandler handler)
        {
            using (var httpClient = new HttpClient(handler))
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "http://ip-api.com/json"))
                {
                    var response = await httpClient.SendAsync(request);
                    return await response.Content.ReadAsStringAsync();
                }
            }
        }

        private static void SetCredentailsProxy(WebProxy proxy, ProxyData proxyData)
        {
            if (proxyData.Login != null
                    && proxyData.Password != null)
            {
                ICredentials credentials = new NetworkCredential(proxyData.Login, proxyData.Password);
                proxy.Credentials = credentials;
            }
        }

        public static WebProxy GetWebProxy(ProxyData proxyData)
        {
            WebProxy webProxy;
            try
            {
                webProxy = new WebProxy(proxyData.Host, proxyData.Port);
                SetCredentailsProxy(webProxy, proxyData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}\r\n{ex.StackTrace}");
                webProxy = new WebProxy();
            };
            return webProxy;
        }

        public static async Task<string> GetJsonDataIp(ProxyData proxyData)
        {
            try
            {
                var handler = new HttpClientHandler();
                handler.UseProxy = true;
                handler.Proxy = GetWebProxy(proxyData);
                string response = await RequestToIp(handler);
                return response;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
