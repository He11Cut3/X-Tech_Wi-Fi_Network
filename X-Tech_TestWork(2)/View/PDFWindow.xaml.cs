using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using X_Tech_TestWork_2_.ViewModel;

namespace X_Tech_TestWork_2_.View
{
    /// <summary>
    /// Логика взаимодействия для PDFWindow.xaml
    /// </summary>
    public partial class PDFWindow : Window
    {
        public PDFWindow()
        {
            InitializeComponent();
            this.DataContext = new PDFViewModel();
        }
        public string TextBoxText { get; set; }

        private void TextBoxInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Zа-яА-Я0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
