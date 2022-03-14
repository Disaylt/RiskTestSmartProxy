using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RiskTestSmartProxy
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string path = $@"{Directory.GetCurrentDirectory()}\Proxies.json";
            string content = FileStream.GetContent(path);
            List<ProxyData> proxies = ProjectJsonHandler.GetJsonObject<List<ProxyData>>(content);
            List<ProxyData> smartProxies = SmartProxyPortAdapter.GetProxiesPort(proxies[0], 10000, 10010);
            foreach(ProxyData proxy in smartProxies)
            {
                string ipInfo = await ProxyCheker.GetJsonDataIp(proxy);
            }
            Console.ReadLine();
        }
    }
}
