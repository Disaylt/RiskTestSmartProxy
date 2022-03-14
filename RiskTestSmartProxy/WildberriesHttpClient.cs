using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiskTestSmartProxy
{
    public class WildberriesHttpClient : ProxyClient, IMarketplaceParser
    {
        public WildberriesHttpClient(List<ProxyData> proxies) : base(proxies)
        {
        }

        public Task<string> GetHtmlProductPage(string adrticle)
        {
            throw new NotImplementedException();
        }
    }
}
