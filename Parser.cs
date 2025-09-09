using NLog;
using Microsoft.VisualBasic;
using Npgsql;
using ParserWithDBConnection.Core;
using ParserWithDBConnection.SiteSettings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace ParserWithDBConnection
{
    public partial class Parser : Form
    {
        private static int _inputBoxCallsCount = 0; 
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        public Parser()
        {
            InitializeComponent();
        }

        private async void buttonParse_Click(object sender, EventArgs e)
        {
            try
            {
                ParserWorker parser = null;
                string parserType = "";

                switch (checkedListBoxSites.CheckedIndices[0])
                {
                    case 0:
                        parser = new ParserWorker { ParserSettings = new StocksSettings() };
                        parserType = "Stocks";
                        break;
                    case 1:
                        parser = new ParserWorker { ParserSettings = new ETFSettings() };
                        parserType = "ETF";
                        break;
                    case 2:
                        parser = new ParserWorker { ParserSettings = new WarrantsSettings() };
                        parserType = "Warrants";
                        break;
                    case 3:
                        parser = new ParserWorker { ParserSettings = new IndicesSettings() };
                        parserType = "Indices";
                        break;
                }

                await parser.Worker(parserType);

                textBoxInfo.Text += $"{Environment.NewLine}{DateTime.Now}: {parserType} завершил парсинг\n";
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex, "Критическая ошибка в работе парсера");
                MessageBox.Show($"Критическая ошибка в работе парсера: {ex.Message}");
                textBoxInfo.Text += $"{Environment.NewLine}{DateTime.Now}: Ошибка - {ex.Message}\n";
            }
        }

        private void checkedListBoxSites_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
       
                for (int i = 0; i < checkedListBoxSites.Items.Count; i++)
                {
                    if (i != e.Index && checkedListBoxSites.GetItemChecked(i))
                    {
                        checkedListBoxSites.SetItemChecked(i, false);
                    }
                }
            }
        }

        private void buttonCopyToDB_Click(object sender, EventArgs e)
        {
            string newDbName = null;
            if (_inputBoxCallsCount == 0)
            {
                newDbName = Interaction.InputBox(
                    "Желаете поменять имя БД?",
                    "Настройка имени базы данных",
                    "ParsedData"
                    );
                _inputBoxCallsCount++;
            }
            newDbName = string.IsNullOrEmpty(newDbName) ? "ParsedData" : newDbName;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CSV");

                openFileDialog.Filter = "CSV files (*.csv)|*.csv|Text files (*.txt)|*.txt|All files (*.*)|*.*";

                openFileDialog.FilterIndex = 1;
                openFileDialog.Title = "Выберите CSV файл";
                openFileDialog.Multiselect = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string csvFile = openFileDialog.FileName;
                    _logger.Info($"Выбран файл {csvFile} для импорта в БД");

                    MessageBox.Show($"Выбран файл: {csvFile}", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    try
                    {
                        DatabaseWorker.CreateDatabase(newDbName);

                        string connectionString = $"Host=localhost;Database={newDbName};Username=postgres";
                        string tableName = Path.GetFileNameWithoutExtension(csvFile);

                        DatabaseWorker.WorkWithDb(connectionString, newDbName, csvFile, tableName);
                        textBoxInfo.Text += $"{Environment.NewLine}{DateTime.Now}: Данные из {csvFile} скопированы в {tableName}\n\n";
                    }
                    catch(Exception ex)
                    {
                        _logger.Fatal("Критическая ошибка при работе с БД");
                    }
                }
            }
        }
    }
}
