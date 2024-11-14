using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using X_Tech_TestWork_2_.Model;

namespace X_Tech_TestWork_2_.ViewModel
{
    public class PDFViewModel : INotifyPropertyChanged
    {
        private string _textBoxText;

        public PDFViewModel()
        {
            Document = new DocumentModel();
            PrintCommand = new RelayCommand(PrintDocument);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string TextBoxText
        {
            get => _textBoxText;
            set
            {
                if (_textBoxText != value)
                {
                    _textBoxText = value;
                    OnPropertyChanged(nameof(TextBoxText)); 
                    OnPropertyChanged(nameof(LabelText)); 
                }
            }
        }

        public string LabelText => TextBoxText;

        public DocumentModel Document { get; }

        public ICommand PrintCommand { get; }

        public void PrintDocument()
        {
            var documentTitle = TextBoxText + ".pdf";

            var pdfGenerator = new PdfGenerator();
            pdfGenerator.CreatePdfFile(TextBoxText, Document);

            MessageBox.Show($"Документ {TextBoxText}.pdf был успешно создан на рабочем столе.");
        }


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
