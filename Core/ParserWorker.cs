using AngleSharp.Browser;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using AngleSharp.Io;
using NLog;
using ParserWithDBConnection.SiteParsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserWithDBConnection.Core
{
    internal class ParserWorker
    {

        private static Logger _logger = LogManager.GetCurrentClassLogger();
        SiteParser parser = new SiteParser();
        IParserSettings parserSettings; 
        HtmlLoader loader;

        public SiteParser Parser 
        { 
            get { return parser; } 
            set { parser = value; } 
        }

        public IParserSettings ParserSettings 
        { 
            get { return parserSettings; }
            set 
            { 
                parserSettings = value;
                loader = new HtmlLoader(value);
            }
        }

        public async Task Worker(string siteName)
        {
            _logger.Info("Начало работы парсера");
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string directoryName = "CSV";
            string directoryPath = Path.Combine(baseDirectory, directoryName);

            if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);

            string fileName = $"{siteName}_{DateTime.Now:yyyyMMdd}.csv";
            string csvFilePath = Path.Combine(directoryPath, fileName);

            try
            {
                using (var sw = new StreamWriter(csvFilePath, false, Encoding.UTF8))
                {
                    _logger.Debug("Загрузка первой страницы для заголовков");
                    string source = await loader.GetSourceByPage(1);
                    IHtmlDocument document = await new HtmlParser().ParseDocumentAsync(source);

                    var headers = parser.Parse(document, "th");
                    _logger.Debug($"Найдено {headers.Length} заголовков");

                    foreach (string line in headers)
                    {
                        sw.WriteLine(line);
                    }

                    int pageCount = parser.FindLastPageNumber(document);
                    _logger.Info($"Всего страниц для обработки: {pageCount}");

                    for (ParserSettings.PageNumber = 1; ParserSettings.PageNumber <= pageCount; ParserSettings.PageNumber++)
                    {
                        _logger.Trace($"Обработка страницы {ParserSettings.PageNumber}/{pageCount}");
                        source = await loader.GetSourceByPage(ParserSettings.PageNumber);
                        document = await new HtmlParser().ParseDocumentAsync(source);
                        string[] rows = parser.Parse(document, "td");

                        _logger.Debug($"На странице {ParserSettings.PageNumber} найдено {rows.Length} строк");

                        foreach (string line in rows)
                        {
                            sw.WriteLine(line);
                        }
                    }
                }
                _logger.Info($"Данные успешно сохранены в {Path.GetFullPath(csvFilePath)}");
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex, "Критическая ошибка в работе парсера");
            }
        }
    }
}
