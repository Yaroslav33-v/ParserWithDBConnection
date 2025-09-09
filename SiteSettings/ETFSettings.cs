using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParserWithDBConnection.Core;

namespace ParserWithDBConnection.SiteSettings
{
    internal class ETFSettings : IParserSettings // Биржевые фонды
    {
        public string BaseUrl { get; set; } = "https://www.wienerborse.at/en/exchange-traded-funds/?c8001-page={CurrentPage}&per-page=50";
        public int PageNumber { get; set; }
    }
}
