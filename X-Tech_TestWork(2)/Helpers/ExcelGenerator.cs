using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Font;
using System;
using System.IO;
using System.Collections.Generic;
using X_Tech_TestWork_2_.Model;

public class PdfGenerator
{
    public void CreatePdfFile(string filename, DocumentModel document)
    {
        if (document == null || document.Persons == null || document.Persons.Count == 0)
        {
            Console.WriteLine("DocumentModel or Persons list is null or empty. Adding test data for demonstration.");
            document = new DocumentModel
            {
                Persons = new List<Person>
                {
                    new Person { Number = 1, LastName = "Иванов", FirstName = "Иван" },
                    new Person { Number = 2, LastName = "Сидоров", FirstName = "Сидор" },
                    new Person { Number = 3, LastName = "Петров", FirstName = "Петр" }
                }
            };
        }
        var pdfPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"{filename}.pdf");

        try
        {
            using (var writer = new PdfWriter(pdfPath))
            {
                using (var pdf = new PdfDocument(writer))
                {
                    var documentLayout = new Document(pdf);
                    string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIAL.TTF");
                    PdfFont font = PdfFontFactory.CreateFont(fontPath, "Identity-H");
                    documentLayout.SetFont(font);

                    documentLayout.Add(new Paragraph("Таблица 1")
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFontSize(18)
                        .SetBold()
                        .SetFont(font));

                    documentLayout.Add(new Paragraph("\n"));

                    Table table = new Table(UnitValue.CreatePercentArray(new float[] { 1, 2, 2 }))
                        .UseAllAvailableWidth();

                    table.AddHeaderCell(new Cell().Add(new Paragraph("Номер")).SetBackgroundColor(iText.Kernel.Colors.ColorConstants.LIGHT_GRAY).SetFont(font));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Фамилия")).SetBackgroundColor(iText.Kernel.Colors.ColorConstants.LIGHT_GRAY).SetFont(font));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Имя")).SetBackgroundColor(iText.Kernel.Colors.ColorConstants.LIGHT_GRAY).SetFont(font));

                    foreach (var row in document.Persons)
                    {
                        table.AddCell(new Cell().Add(new Paragraph(row.Number?.ToString() ?? string.Empty).SetFont(font)));
                        table.AddCell(new Cell().Add(new Paragraph(row.LastName ?? string.Empty).SetFont(font)));
                        table.AddCell(new Cell().Add(new Paragraph(row.FirstName ?? string.Empty).SetFont(font)));
                    }

                    documentLayout.Add(table);
                    documentLayout.Add(new Paragraph("\n"));
                    documentLayout.Add(new Paragraph(filename)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFontSize(16)
                        .SetBold()
                        .SetFont(font));

                    documentLayout.Add(new Paragraph("\n"));

                    Table table2 = new Table(UnitValue.CreatePercentArray(new float[] { 1, 2, 2 }))
                        .UseAllAvailableWidth();

                    table2.AddHeaderCell(new Cell().Add(new Paragraph("Номер")).SetBackgroundColor(iText.Kernel.Colors.ColorConstants.LIGHT_GRAY).SetFont(font));
                    table2.AddHeaderCell(new Cell().Add(new Paragraph("Фамилия")).SetBackgroundColor(iText.Kernel.Colors.ColorConstants.LIGHT_GRAY).SetFont(font));
                    table2.AddHeaderCell(new Cell().Add(new Paragraph("Имя")).SetBackgroundColor(iText.Kernel.Colors.ColorConstants.LIGHT_GRAY).SetFont(font));

                    foreach (var row in document.Persons)
                    {
                        table2.AddCell(new Cell().Add(new Paragraph(row.Number?.ToString() ?? string.Empty).SetFont(font)));
                        table2.AddCell(new Cell().Add(new Paragraph(row.LastName ?? string.Empty).SetFont(font)));
                        table2.AddCell(new Cell().Add(new Paragraph(row.FirstName ?? string.Empty).SetFont(font)));
                    }

                    documentLayout.Add(table2);

                    documentLayout.Close();
                }
            }

            Console.WriteLine($"PDF документ успешно создан по пути: {pdfPath}");
        }
        catch (IOException ioEx)
        {
            Console.WriteLine($"Ошибка ввода/вывода файла: {ioEx.Message}");
            throw new Exception("Ошибка ввода/вывода при создании PDF файла", ioEx);
        }
        catch (ArgumentException argEx)
        {
            Console.WriteLine($"Ошибка аргумента: {argEx.Message}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла непредвиденная ошибка: {ex.Message}");
            throw new Exception("Непредвиденная ошибка при создании PDF файла", ex);
        }
    }
}
