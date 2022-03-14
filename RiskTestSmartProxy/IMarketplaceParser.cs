using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiskTestSmartProxy
{
    public interface IMarketplaceParser
    {
        Task<string> GetHtmlProductPage(string adrticle);
    }
}
