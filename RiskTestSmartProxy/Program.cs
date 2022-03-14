using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace RiskTestSmartProxy
{
    enum Markets
    {
        Amazon,
        WB
    }
    internal class Program
    {
        private static Dictionary<Markets, IMarketplaceParser> _marketsAndParser;
        private static Dictionary<Markets, string[]> _marketsWithArticles;
        private static Dictionary<Markets, IMarketplaceParser> marketsAndParser
        {
            get
            {
                if(_marketsAndParser == null)
                {
                    var proxies = GetSmartProxies();
                    Dictionary<Markets, IMarketplaceParser> marketsAndParser = new Dictionary<Markets, IMarketplaceParser>();
                    marketsAndParser.Add(Markets.Amazon, new AmazonHttpClient(proxies));
                    marketsAndParser.Add(Markets.WB, new WildberriesHttpClient(proxies));
                    _marketsAndParser = marketsAndParser;
                }
                return _marketsAndParser;
            }
        }
        private static Dictionary<Markets, string[]> marketsWithArticles
        {
            get
            {
                if(_marketsWithArticles == null)
                {
                    Dictionary<Markets, string[]> marketsWithArticles = new Dictionary<Markets, string[]>();
                    marketsWithArticles.Add(Markets.Amazon, GetArticles("Amazon"));
                    marketsWithArticles.Add(Markets.WB, GetArticles("WB"));
                    _marketsWithArticles = marketsWithArticles;
                }
                return _marketsWithArticles;
            }
        }
        static async Task Main(string[] args)
        {
            Random random = new Random();
            int parsCount = ReadNumPars();
            for (int parsNum = 0; parsNum < parsCount; parsNum++)
            {
                int chooseNumMarket = random.Next(0, 1);
                Markets marketplace = (Markets)chooseNumMarket;
                string article = ChooseArticle(random, marketplace);
                string pageContent = await marketsAndParser[marketplace].GetHtmlProductPage(article);
                Console.WriteLine($"{parsNum} - {pageContent.Substring(0, 10)}");
            }
            Console.ReadLine();
        }

        static string ChooseArticle(Random random, Markets marketplace)
        {
            int randomNum = random.Next(0, marketsWithArticles[marketplace].Count());
            string artcile = marketsWithArticles[marketplace][randomNum];
            return artcile;
        }

        static int ReadNumPars()
        {
            string text = Console.ReadLine();
            if(int.TryParse(text, out int num))
            {
                return num;
            }
            else
            {
                return 0;
            }
        }

        static List<ProxyData> GetSmartProxies()
        {
            string path = $@"{Directory.GetCurrentDirectory()}\Proxies.json";
            string content = FileStream.GetContent(path);
            List<ProxyData> proxies = ProjectJsonHandler.GetJsonObject<List<ProxyData>>(content);
            List<ProxyData> smartProxies = SmartProxyPortAdapter.GetProxiesPort(proxies[0], 10001, 19999);
            return smartProxies;
        }

        static string[] GetArticles(string marketpalace)
        {
            string path = $@"{Directory.GetCurrentDirectory()}\Articles\{marketpalace}.txt";
            string[] articles = FileStream.GetContentLines(path);
            return articles;
        }
    }
}
