using System;
using System.IO;
using iText.IO.Font;
using iText.IO.Util;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout.Renderer;
using ETT.Web.Models.Record;
using System.Collections.Generic;
using ETT.Web.Models.Report;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Linq;

namespace ETT.Web.Util.Reporting
{
    public class ToPDF
    {

        public String DEST = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\recordPDF.pdf";
        public void RecordsToPDF(IEnumerable<RecordViewModel> records)
        {
            RecordListViewModel a = new RecordListViewModel()
            {
                Records = records
            };
            FileInfo file = new FileInfo(DEST);
            if (!file.Directory.Exists) file.Directory.Create();
            new ToPDF().CreatePdf(DEST, a);
        }
        public virtual void CreatePdf(String dest, RecordListViewModel records)
        {
            //Initialize PDF writer
            PdfWriter writer = new PdfWriter(dest);
            //Initialize PDF document
            PdfDocument pdf = new PdfDocument(writer);
            // Initialize document
            Document document = new Document(pdf);
            // Create a PdfFont
            PdfFont font = PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.TIMES_ROMAN);
            // Add a Paragraph
            document.Add(new Paragraph("Record report").SetFont(font).SetFontSize(18).SetTextAlignment(TextAlignment.CENTER));
            // Add ListItem objects
            float[] columnWidths = { 2, 2, 2, 5, 2, 4 };
            Table table = new Table(columnWidths, true);
            table.SetFontSize(11);
            table.AddCell("Record ID");
            table.AddCell("Project Name");
            table.AddCell("Task Name");
            table.AddCell("Note");
            table.AddCell("Work time");
            table.AddCell("Start date and time");
            foreach (var record in records.Records)
            {
                table.AddCell(record.Id.ToString());
                table.AddCell(record.ProjectName);
                table.AddCell(record.TaskName.ToString());
                table.AddCell(record.Note);
                table.AddCell(record.Hours.ToString());
                table.AddCell(record.RecordDateTime.ToString());
            }
            table.SetBorder(new SolidBorder(1));
            document.Add(table);

            //Close document
            document.Close();
        }
        public void ReportRecordsToPDF(IEnumerable<ReportRecordsViewModel> records)
        {
            FileInfo file = new FileInfo(DEST);
            if (!file.Directory.Exists) file.Directory.Create();
            new ToPDF().CreateReportPdf(DEST, records);
        }
        public void CreateReportPdf(String dest, IEnumerable<ReportRecordsViewModel> records)
        {
            //Initialize PDF writer
            PdfWriter writer = new PdfWriter(dest);
            //Initialize PDF document
            PdfDocument pdf = new PdfDocument(writer);
            // Initialize document
            Document document = new Document(pdf);
            // Create a PdfFont
            PdfFont font = PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.TIMES_ROMAN);
            PdfFont font1 = PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.TIMES_ROMAN, "Cp1251", true);
            //string arialuniTff = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIALUNI.TTF");
            //Register the font with iTextSharp
            //PdfFontFactory.Register(arialuniTff);
            // Add a Paragraph
            string projectName = null;
            foreach (var item in records)
            {
                projectName = item.ProjectName;
            }
            document.Add(new Paragraph("Report").SetFont(font1).SetFontSize(18).SetTextAlignment(TextAlignment.CENTER));
            document.Add(new Paragraph("Project Name: " + projectName).SetFont(font1).SetFontSize(18).SetTextAlignment(TextAlignment.CENTER));
            float[] columnWidths = { 2, 4, 3, 5, 2, 3 };
            Table table = new Table(columnWidths, true);
            table.SetFont(font1);
            table.SetFontSize(11);
            table.AddCell("Record ID");
            table.AddCell("User Email");
            table.AddCell("Task Name");
            table.AddCell("Note");
            table.AddCell("Work time");
            table.AddCell("Start date and time");
            foreach (var record in records)
            {
                table.AddCell(record.Id.ToString());
                table.AddCell(record.UserName);
                table.AddCell(record.TaskName.ToString());
                table.AddCell(record.Note);
                table.AddCell(record.Hours.ToString());
                table.AddCell(record.RecordDateTime.ToString()).SetBorderBottom(new SolidBorder(1));
            }
            document.Add(table);

            //Close document
            document.Close();
        }

        public MemoryStream GeneratePdf(IEnumerable<ReportRecordsViewModel> records)
        {
            MemoryStream ms = new MemoryStream();
            PdfWriter writer = new PdfWriter(ms);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);
            PdfFont font = PdfFontFactory.CreateFont("C:\\Windows\\Fonts\\Arial.ttf", PdfEncodings.IDENTITY_H);

            string projectName = records.FirstOrDefault().ProjectName;

            document.Add(new Paragraph("Report").SetFont(font).SetFontSize(18).SetTextAlignment(TextAlignment.CENTER));
            document.Add(new Paragraph("Project Name: " + projectName).SetFont(font).SetFontSize(18).SetTextAlignment(TextAlignment.CENTER));
            float[] columnWidths = { 2, 4, 3, 5, 2, 3 };
            Table table = new Table(columnWidths, true);
            table.SetFont(font);
            table.SetFontSize(11);
            table.AddCell("Record ID");
            table.AddCell("User Email");
            table.AddCell("Task Name");
            table.AddCell("Note");
            table.AddCell("Work time");
            table.AddCell("Start date and time");
            foreach (var record in records)
            {
                table.AddCell(record.Id.ToString());
                table.AddCell(record.UserName);
                table.AddCell(record.TaskName.ToString());
                table.AddCell(record.Note);
                table.AddCell(record.Hours.ToString());
                table.AddCell(record.RecordDateTime.ToString()).SetBorderBottom(new SolidBorder(1));
            }

            document.Add(table);
            document.Close();
            return ms;
        }
        //public void ReportRecordsToExcel(IEnumerable<ReportRecordsViewModel> records)
        //{
        //    // Set the file name and get the output directory
        //    var fileName = "Example-CRM-" + DateTime.Now.ToString("yyyy-MM-dd--hh-mm-ss") + ".xlsx";
        //    var outputDir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        //    // Create the file using the FileInfo object
        //    var file = new FileInfo(outputDir + "\\" + fileName);
        //    if (!file.Directory.Exists) file.Directory.Create();
        //    using (var package = new ExcelPackage(file))
        //    {
        //        // add a new worksheet to the empty workbook
        //        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sales list - " + DateTime.Now.ToShortDateString());

        //        // --------- Data and styling goes here -------------- //
        //        worksheet.TabColor = System.Drawing.Color.Blue;
        //        worksheet.DefaultRowHeight = 12;
        //        worksheet.HeaderFooter.FirstFooter.LeftAlignedText = string.Format("Generated: {0}", DateTime.Now.ToShortDateString());
        //        worksheet.Row(1).Height = 20;
        //        worksheet.Row(2).Height = 18;
        //        // Start adding the header
        //        // First of all the first row
        //        worksheet.Cells[1, 1].Value = "Company name";
        //        worksheet.Cells[1, 2].Value = "Address";
        //        worksheet.Cells[1, 3].Value = "Status (unstyled)";
        //        worksheet.Cells[1, 1].Value = "Record ID";
        //        worksheet.Cells[1, 2].Value = "User Email";
        //        worksheet.Cells[1, 3].Value = "Task name";
        //        worksheet.Cells[1, 4].Value = "Note";
        //        worksheet.Cells[1, 5].Value = "Work time";
        //        worksheet.Cells[1, 6].Value = "Start date";
        //        using (var range = worksheet.Cells[1, 1, 1, 6])
        //        {
        //            range.Style.Border.BorderAround(ExcelBorderStyle.Thick);
        //            range.Style.Fill.PatternType = ExcelFillStyle.Solid;
        //            range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Gray);
        //            range.Style.Font.Bold = true;
        //            range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        //        }
        //        int rowNumber = 2;
        //        foreach (var record in records)
        //        {
        //            worksheet.Cells[rowNumber, 1].Value = record.Id;
        //            worksheet.Cells[rowNumber, 2].Value = record.UserName;
        //            worksheet.Cells[rowNumber, 3].Value = record.TaskName;
        //            worksheet.Cells[rowNumber, 4].Value = record.Note;
        //            worksheet.Cells[rowNumber, 5].Value = record.Hours;
        //            worksheet.Cells[rowNumber, 6].Value = record.RecordDateTime.ToString();
        //            rowNumber++;
        //        }
        //        worksheet.Cells[1, 1, rowNumber - 1, 6].Style.Border.BorderAround(ExcelBorderStyle.Thick);
        //        worksheet.Cells[1, 2, rowNumber - 1, 2].Style.Border.BorderAround(ExcelBorderStyle.Thick);
        //        worksheet.Cells[1, 4, rowNumber - 1, 4].Style.Border.BorderAround(ExcelBorderStyle.Thick);
        //        worksheet.Cells[1, 6, rowNumber - 1, 6].Style.Border.BorderAround(ExcelBorderStyle.Thick);
        //        worksheet.Column(1).AutoFit();
        //        worksheet.Column(2).AutoFit();
        //        worksheet.Column(3).AutoFit();
        //        worksheet.Column(4).AutoFit();
        //        worksheet.Column(5).AutoFit();
        //        worksheet.Column(6).AutoFit();
        //        package.Save();
        //    }
        //}
        public ExcelPackage GenerateExcel(IEnumerable<ReportRecordsViewModel> records)
        {
           // Create excel 
            ExcelPackage package = new ExcelPackage();

            // add a new worksheet to the empty workbook
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Report " + DateTime.Now.ToShortDateString());

            //------------------Data goes here----------------//
            // Add project name header
            worksheet.Cells[1, 1, 1, 6].Merge = true;
            worksheet.Cells[1, 1].Value = "Project name: " + records.FirstOrDefault().ProjectName;

            // Add header Colomn names
            worksheet.Cells[2, 1].Value = "Company name";
            worksheet.Cells[2, 2].Value = "Address";
            worksheet.Cells[2, 3].Value = "Status (unstyled)";
            worksheet.Cells[2, 1].Value = "Record ID";
            worksheet.Cells[2, 2].Value = "User Email";
            worksheet.Cells[2, 3].Value = "Task name";
            worksheet.Cells[2, 4].Value = "Note";
            worksheet.Cells[2, 5].Value = "Work time";
            worksheet.Cells[2, 6].Value = "Start date";

            //Adding rows of data
            int rowNumber = 3;
            foreach (var record in records)
            {
                worksheet.Cells[rowNumber, 1].Value = record.Id;
                worksheet.Cells[rowNumber, 2].Value = record.UserName;
                worksheet.Cells[rowNumber, 3].Value = record.TaskName;
                worksheet.Cells[rowNumber, 4].Value = record.Note;
                worksheet.Cells[rowNumber, 5].Value = record.Hours;
                worksheet.Cells[rowNumber, 6].Value = record.RecordDateTime.ToString();
                rowNumber++;
            }

            //------------------Style goes here----------------//
            // Set style for Project name
            using (var range = worksheet.Cells[1, 1])
            {
                range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                range.Style.Font.Size = 13;
                range.Style.Font.Bold = true;
            }
            worksheet.Row(1).Height = 20;

            // Set style for colomn names 
            using (var range = worksheet.Cells[2, 1, 2, 6])
            {
                range.Style.Border.BorderAround(ExcelBorderStyle.Thick);
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Gray);
                range.Style.Font.Bold = true;
                range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            }
            worksheet.Row(2).Height = 18;

            // Set style for other and general content
            worksheet.DefaultRowHeight = 12;
            worksheet.HeaderFooter.FirstFooter.LeftAlignedText = string.Format("Generated: {0}", DateTime.Now.ToShortDateString());
            using (var range = worksheet.Cells[2, 1, 2, 6])
            {
                range.Style.Border.BorderAround(ExcelBorderStyle.Thick);
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Gray);
                range.Style.Font.Bold = true;
                range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            }
            worksheet.Cells[1, 1, rowNumber - 1, 6].Style.Border.BorderAround(ExcelBorderStyle.Thick);
            worksheet.Cells[2, 1, rowNumber - 1, 6].Style.Border.BorderAround(ExcelBorderStyle.Thick);
            worksheet.Cells[2, 2, rowNumber - 1, 2].Style.Border.BorderAround(ExcelBorderStyle.Thick);
            worksheet.Cells[2, 4, rowNumber - 1, 4].Style.Border.BorderAround(ExcelBorderStyle.Thick);
            worksheet.Cells[2, 6, rowNumber - 1, 6].Style.Border.BorderAround(ExcelBorderStyle.Thick);
            worksheet.Cells[2, 1, 2, 6].Style.Font.Size = 12;
            worksheet.Cells[1, 1, rowNumber - 1, 6].Style.Font.Name = "Times New Roman";
            worksheet.Cells[2, 1, rowNumber - 1, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
            worksheet.Cells[1, 1, rowNumber-1, 6].AutoFitColumns();

            // set properties of worksheet
            package.Workbook.Properties.Title = "Report";
            package.Workbook.Properties.Subject = "Records report";
            package.Workbook.Properties.Keywords = "report records";

            return package;
        }
    }
}
