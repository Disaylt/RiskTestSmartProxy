using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RiskTestSmartProxy
{
    public class ProxyClient
    {
        private readonly Random _random;
        private readonly List<ProxyData> _proxies;
        protected WebProxy ChooseProxy
        {
            get
            {
                int randomNumProxy = _random.Next(0, _proxies.Count);
                WebProxy webProxy = ProxyCheker.GetWebProxy(_proxies[randomNumProxy]);
                return webProxy;
            }
        }
        public ProxyClient(List<ProxyData> proxies)
        {
            _random = new Random();
            _proxies = proxies;
        }
    }
}
