using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using X_Tech_TestWork_1_.Model;

namespace X_Tech_TestWork.Helpers
{
    public class WifiDatabaseHelper
    {
        private readonly string _connectionString;

        public WifiDatabaseHelper()
        {
            try
            {
                var configFile = "Db_Connect.config";
                var map = new ExeConfigurationFileMap { ExeConfigFilename = configFile };
                Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
                _connectionString = config.ConnectionStrings.ConnectionStrings["WifiDbConnection"].ConnectionString;
            }
            catch (ConfigurationErrorsException ex)
            {
                MessageBox.Show($"Ошибка загрузки конфигурации: {ex.Message}", "Ошибка конфигурации", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Неизвестная ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void SaveNetworks(IEnumerable<WifiDatabase> networks)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    foreach (var network in networks)
                    {
                        string query = @"
                        IF EXISTS (SELECT 1 FROM WifiNetworks WHERE SSID = @SSID)
                        BEGIN
                            UPDATE WifiNetworks 
                            SET SignalStrength = @SignalStrength
                            WHERE SSID = @SSID
                        END
                        ELSE
                        BEGIN
                            INSERT INTO WifiNetworks (SSID, SignalStrength) 
                            VALUES (@SSID, @SignalStrength)
                        END";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@SSID", network.SSID);
                            command.Parameters.AddWithValue("@SignalStrength", network.SignalStrength);

                            try
                            {
                                command.ExecuteNonQuery();
                            }
                            catch (SqlException ex)
                            {
                                MessageBox.Show($"Ошибка выполнения SQL-запроса: {ex.Message}", "Ошибка SQL", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Неизвестная ошибка при сохранении данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }

                    MessageBox.Show("Сети успешно сохранены.", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Ошибка подключения к базе данных: {ex.Message}", "Ошибка подключения", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Неизвестная ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
