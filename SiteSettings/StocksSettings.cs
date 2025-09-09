using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParserWithDBConnection.Core;

namespace ParserWithDBConnection.SiteSettings
{
    internal class StocksSettings : IParserSettings // Основной рынок - акции
    {
        public string BaseUrl { get; set; } = "https://www.wienerborse.at/en/stocks-prime-market/";
        public int PageNumber { get; set; }
    }
}
