using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RiskTestSmartProxy
{
    public class AmazonHttpClient : ProxyClient, IMarketplaceParser
    {
        public AmazonHttpClient(List<ProxyData> proxies) : base(proxies)
        {
        }

        public async Task<string> GetHtmlProductPage(string adrticle)
        {
            var handler = new HttpClientHandler();
            handler.UseProxy = true;
            handler.Proxy = ChooseProxy();

            using (var httpClient = new HttpClient(handler))
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"https://www.amazon.com/dp/{adrticle}"))
                {
                    request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:98.0) Gecko/20100101 Firefox/98.0");
                    request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,*/*;q=0.8");
                    request.Headers.TryAddWithoutValidation("Accept-Language", "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
                    request.Headers.TryAddWithoutValidation("Accept-Encoding", "UTF-8");
                    request.Headers.TryAddWithoutValidation("Connection", "keep-alive");
                    request.Headers.TryAddWithoutValidation("Upgrade-Insecure-Requests", "1");
                    request.Headers.TryAddWithoutValidation("Sec-Fetch-Dest", "document");
                    request.Headers.TryAddWithoutValidation("Sec-Fetch-Mode", "navigate");
                    request.Headers.TryAddWithoutValidation("Sec-Fetch-Site", "none");
                    request.Headers.TryAddWithoutValidation("Sec-Fetch-User", "?1");
                    request.Headers.TryAddWithoutValidation("TE", "trailers");

                    var response = await httpClient.SendAsync(request);
                    return await response.Content.ReadAsStringAsync();
                }
            }
        }
    }
}
