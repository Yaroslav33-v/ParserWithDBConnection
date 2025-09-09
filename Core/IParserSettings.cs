using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserWithDBConnection.Core
{
    internal interface IParserSettings
    {
        string BaseUrl { get; set; }
        int PageNumber { get; set; }
    }
}
