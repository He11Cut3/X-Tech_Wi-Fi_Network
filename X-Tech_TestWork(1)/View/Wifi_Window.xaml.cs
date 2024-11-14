using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using X_Tech_TestWork_1_.ViewModel;

namespace X_Tech_TestWork_1_.View
{
    /// <summary>
    /// Логика взаимодействия для Wifi_Window.xaml
    /// </summary>
    public partial class Wifi_Window : Window
    {
        public Wifi_Window()
        {
            InitializeComponent();
            DataContext = new WifiViewModel();
        }
    }
}
