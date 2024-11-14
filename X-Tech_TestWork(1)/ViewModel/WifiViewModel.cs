using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using X_Tech_TestWork_1_.Helpers;
using X_Tech_TestWork_1_.Model;
using X_Tech_TestWork.Helpers;

namespace X_Tech_TestWork_1_.ViewModel
{
    public class WifiViewModel : INotifyPropertyChanged
    {
        private WifiDatabaseHelper _wifiDatabase = new WifiDatabaseHelper();
        private WifiScannerHelper _wifiScanner = new WifiScannerHelper();
        private string _bestNetwork;
        public ObservableCollection<WifiDatabase> WifiDatabase { get; set; } = new ObservableCollection<WifiDatabase>();


        public string BestNetwork
        {
            get => _bestNetwork;
            set
            {
                _bestNetwork = value;
                OnPropertyChanged(nameof(BestNetwork));
            }
        }

        public ICommand ScanCommand { get; }
        public ICommand SaveCommand { get; }

        public ICommand CloseCommand { get; }

        public WifiViewModel()
        {
            ScanCommand = new RelayCommand(ScanNetworks);
            SaveCommand = new RelayCommand(SaveNetworks);
        }

        private void ScanNetworks()
        {
            try
            {
                WifiDatabase.Clear();

                var scannedNetworks = _wifiScanner.ScanNetworks().Select(sn => new WifiDatabase
                {
                    SSID = sn.SSID,
                    SignalStrength = sn.SignalStrength
                }).ToList();

                WifiDatabase.Clear();
                foreach (var network in scannedNetworks)
                {
                    WifiDatabase.Add(network);
                }
                var bestNetwork = WifiDatabase.OrderByDescending(n => n.SignalStrength).FirstOrDefault();
                BestNetwork = bestNetwork?.SSID;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сканировании сетей: {ex.Message}", "Ошибка сканирования", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void SaveNetworks()
        {
            try
            {
                _wifiDatabase.SaveNetworks(WifiDatabase.Cast<WifiDatabase>());

            }
            catch (InvalidCastException ex)
            {
                MessageBox.Show($"Ошибка приведения типа: {ex.Message}", "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении сетей: {ex.Message}", "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
