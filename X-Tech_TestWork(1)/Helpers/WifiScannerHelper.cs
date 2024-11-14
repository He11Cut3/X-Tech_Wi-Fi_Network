using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using X_Tech_TestWork_1_.Model;

namespace X_Tech_TestWork_1_.Helpers
{
    public class WifiScannerHelper
    {
        public List<WifiDatabase> ScanNetworks()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var networks = new List<WifiDatabase>();

            try
            {
                var processStartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/C netsh wlan show networks mode=Bssid",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    StandardOutputEncoding = Encoding.GetEncoding(866)
                };

                using (var process = new Process { StartInfo = processStartInfo })
                {
                    process.Start();
                    string output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();

                    var lines = output.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                    WifiDatabase currentNetwork = null;

                    foreach (var line in lines)
                    {
                        if (line.StartsWith("SSID"))
                        {
                            if (currentNetwork != null)
                            {
                                networks.Add(currentNetwork);
                            }

                            currentNetwork = new WifiDatabase
                            {
                                SSID = line.Split(':')[1].Trim()
                            };
                        }
                        else if (line.Contains("Сигнал") || line.Contains("Signal"))
                        {
                            if (currentNetwork != null)
                            {
                                var signalStrength = line.Split(':')[1].Trim().Replace("%", "").Trim();
                                if (int.TryParse(signalStrength, out int signal))
                                {
                                    currentNetwork.SignalStrength = signal;
                                }
                            }
                        }
                    }
                    if (currentNetwork != null)
                    {
                        networks.Add(currentNetwork);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сканировании сетей Wi-Fi: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return networks;
        }

    }

}
