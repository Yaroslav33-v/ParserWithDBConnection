using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using NLog;
using ParserWithDBConnection.Core;

namespace ParserWithDBConnection.SiteParsers
{
    internal class SiteParser
    {

        private static Logger _logger = LogManager.GetCurrentClassLogger();
        public string[] Parse(IHtmlDocument document, string tableElement) 
        {
            List<string> list = new List<string>();
            if (tableElement == "th")
            {
                var tables = document.QuerySelectorAll("thead");
                foreach (var table in tables)
                {
                    var headers = table.QuerySelectorAll(tableElement);
                    string rowText = string.Join("\t", headers.Select(header => header.TextContent.Trim()));
                    list.Add(rowText);
                }
            }
            else if(tableElement == "td")
            {
                var tables = document.QuerySelectorAll("tbody");

                foreach (var table in tables)
                {
                    var rows = table.QuerySelectorAll("tr");

                    foreach (var row in rows)
                    {
                        var cells = row.QuerySelectorAll(tableElement);
                        string rowText = string.Join("\t", cells.Select(cell => cell.TextContent.Trim()));
                        list.Add(rowText);
                    }
                }
            }
            return list.ToArray();
        }
        public int FindLastPageNumber(IHtmlDocument document)
        {
            try
            {
                var pageLinks = document.QuerySelectorAll("li")
                    .Select(x => x.TextContent.Trim())
                    .Where(text => int.TryParse(text, out _))
                    .Select(int.Parse)
                    .ToList();

                int lastPage = pageLinks.Any() ? pageLinks.Max() : 1;
                _logger.Debug($"Найдено страниц: {lastPage}");
                return lastPage;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Ошибка при определении количества страниц");
                return 1;
            }
        }
    }
}
