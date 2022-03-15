using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RiskTestSmartProxy
{
    public class WildberriesHttpClient : ProxyClient, IMarketplaceParser
    {
        public WildberriesHttpClient(List<ProxyData> proxies) : base(proxies)
        {
        }

        public async Task<string> GetHtmlProductPage(string article)
        {
            var httpClientHandler = new HttpClientHandler
            {
                UseProxy = true
            };
            httpClientHandler.Proxy = ChooseProxy;
            try
            {
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"https://wbx-content-v2.wbstatic.net/ru/{article}.json"))
                    {
                        request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:98.0) Gecko/20100101 Firefox/98.0");
                        request.Headers.TryAddWithoutValidation("Accept", "*/*");
                        request.Headers.TryAddWithoutValidation("Accept-Language", "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
                        request.Headers.TryAddWithoutValidation("Accept-Encoding", "UTF-8");
                        request.Headers.TryAddWithoutValidation("Origin", "https://www.wildberries.ru");
                        request.Headers.TryAddWithoutValidation("Connection", "keep-alive");
                        request.Headers.TryAddWithoutValidation("Sec-Fetch-Dest", "empty");
                        request.Headers.TryAddWithoutValidation("Sec-Fetch-Mode", "cors");
                        request.Headers.TryAddWithoutValidation("Sec-Fetch-Site", "cross-site");

                        var response = await httpClient.SendAsync(request);
                        return await response.Content.ReadAsStringAsync();
                    }
                }
            }
            catch
            {
                Console.WriteLine($"{article} - not parse");
                return string.Empty;
            }
        }
    }
}
