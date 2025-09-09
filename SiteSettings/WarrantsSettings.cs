using ParserWithDBConnection.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserWithDBConnection.SiteSettings
{
    internal class WarrantsSettings : IParserSettings
    {
        public string BaseUrl { get; set; } = "https://www.wienerborse.at/en/warrants/?c10952-page={CurrentPage}&per-page=50";
        public int PageNumber { get; set; }
    }
}
