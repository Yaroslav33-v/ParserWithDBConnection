using NLog;
using Npgsql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParserWithDBConnection.Core
{
    internal class DatabaseWorker
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        public static void CreateDatabase(string newDbName)
        {
            using (var masterConn = new NpgsqlConnection("Host=localhost;Database=postgres;Username=postgres;Password=your_password"))
            {
                try
                {
                    masterConn.Open();
                    string checkDbQuery = $"SELECT 1 FROM pg_database WHERE datname = '{newDbName}'";

                    using (var checkCmd = new NpgsqlCommand(checkDbQuery, masterConn))
                    {
                        var exists = checkCmd.ExecuteScalar();

                        if (exists == null)
                        {
                            string createDbQuery = $"CREATE DATABASE \"{newDbName}\"";

                            using (var createCmd = new NpgsqlCommand(createDbQuery, masterConn))
                            {
                                createCmd.ExecuteNonQuery();
                                _logger.Info($"Создана БД {newDbName}");
                            }
                        }
                        else
                        {
                            _logger.Info($"БД {newDbName} уже существует");
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error($"Ошибка при создании БД: {ex.Message}");
                    throw;
                }
            }
        }

        public static void WorkWithDb(string connectionString, string DbName, string csvFile, string tableName)
        {
            using (var targetConn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    targetConn.Open();
                    _logger.Info($"Установлено подключение к {DbName}");
                    using (var transaction = targetConn.BeginTransaction())
                    {
                        _logger.Info("Начало транзакции");
                        try
                        {
                            var headers = File.ReadLines(csvFile).First().Split('\t');

                            CreateTable(tableName, headers, targetConn, transaction);

                            TruncateTable(tableName, targetConn, transaction);

                            CopyToTable(tableName, csvFile, targetConn, transaction);

                            transaction.Commit();

                            _logger.Info($"Данные из {csvFile} успешно скопированы в таблицу {tableName}. Транзакция выполнена");
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            _logger.Error("Ошибка при выполнении транзакции. Откат изменений");
                            throw ex;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при подключении", "Ошибка",
                          MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _logger.Error($"Ошибка при подключении к {DbName}");
                    throw ex;
                }
            }
        }

        public static void CreateTable(string tableName, string[] headers, NpgsqlConnection targetConn, NpgsqlTransaction transaction)
        {
            var createTableCommand = $"CREATE TABLE IF NOT EXISTS {tableName} (" +
                                $"{string.Join(", ", headers.Select(h => $"\"{h}\" TEXT"))})";

            using (var cmd = new NpgsqlCommand(createTableCommand, targetConn, transaction))
            {
                cmd.ExecuteNonQuery();
            }
            _logger.Info($"Создана таблица {tableName}, если таковая не существовала");
        }

        public static void TruncateTable(string tableName, NpgsqlConnection targetConn, NpgsqlTransaction transaction)
        {
            using (var cmd = new NpgsqlCommand($"TRUNCATE TABLE {tableName}", targetConn, transaction))
            {
                cmd.ExecuteNonQuery();
            }
            _logger.Info($"Таблица {tableName} очищена");
        }

        public static void CopyToTable(string tableName, string csvFile, NpgsqlConnection targetConn, NpgsqlTransaction transaction)
        {
            var copyCommand = $"COPY {tableName} FROM '{csvFile}' WITH (DELIMITER E'\t', FORMAT CSV, HEADER true, ENCODING 'WIN1251');";

            using (var cmd = new NpgsqlCommand(copyCommand, targetConn, transaction))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }
}
