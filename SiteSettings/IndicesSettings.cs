using ParserWithDBConnection.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserWithDBConnection.SiteSettings
{
    internal class IndicesSettings : IParserSettings
    {
        public string BaseUrl { get; set; } = "https://www.wienerborse.at/en/indices-austria/";
        public int PageNumber { get; set; }
    }
}
