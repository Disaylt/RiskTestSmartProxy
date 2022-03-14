using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiskTestSmartProxy
{
    public static class SmartProxyPortAdapter
    {
        public static List<ProxyData> GetProxiesPort(ProxyData proxyData, int minPort, int MaxPort)
        {
            List<ProxyData> proxies = new List<ProxyData>();
            for(int currentPort = minPort; currentPort < MaxPort; currentPort++)
            {
                ProxyData proxy = new ProxyData
                {
                    Host = proxyData.Host,
                    Port = currentPort,
                    Login = proxyData.Login,
                    Password = proxyData.Password
                };
                proxies.Add(proxy);
            }
            return proxies;
        }
    }
}
