using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.Drawing;
using Syncfusion.XlsIO;
using System.IO;
using pointOfSales.Models;
using point_of_sales.Models;
using Syncfusion.Pdf.Grid;
using System.Data;

namespace pointOfSales.Controllers
{
    public class ReportController : Controller
    {
        public DateTime dateCreated = DateTime.Now;
        private DataContext db = new DataContext();

        //Generate Csv: Report
        public ActionResult csvSheet()
        {
            //if (SaveOption == null)
            //    return View();
            //Step 1 : Instantiate the spreadsheet creation engine.
            ExcelEngine excelEngine = new ExcelEngine();
            IApplication application = excelEngine.Excel;
            //Step 2 : Instantiate the excel application object.
            var ReportName = dateCreated.ToString("yyyy") + "-" + dateCreated.ToString("MM") + "-" + dateCreated.ToString("dd") + "-" + dateCreated.ToString("hh") + "" + dateCreated.ToString("mm") + " " + dateCreated.ToString("ss") + "csv_report";
            //path to create the file to.
            var path = @"C:\\Users\Nolwazi Mlonzi\Desktop\" + ReportName + ".csv";
            //create workbook and worksheet
            IWorkbook workbook = application.Workbooks.Create(1);
            IWorksheet worksheet = workbook.Worksheets[0];
            //populate data to the worksheet
            worksheet.ImportData(db.ProductItems.ToList(), 2, 1, false);
            workbook.SaveAs(path, ",", HttpContext.ApplicationInstance.Response, ExcelDownloadType.Open, ExcelHttpContentType.CSV);
            workbook.Close();

            excelEngine.Dispose();

            return View();
        }

        // Generate Xlsx: Report
        public ActionResult CreateExcelSheet()
        {
            //Instantiate the spreadsheet creation engine.
            ExcelEngine excelEngine = new ExcelEngine();
            //Instantiate the excel application object.
            IApplication application = excelEngine.Excel;
            System.Diagnostics.Debug.WriteLine(" --");
            //constructing reportname using date + time + report string
            var ReportName = dateCreated.ToString("yyyy") + "-" + dateCreated.ToString("MM") + "-" + dateCreated.ToString("dd") + "-" + dateCreated.ToString("hh") + "" + dateCreated.ToString("mm") + " " + dateCreated.ToString("ss") + "xlsx_report";
            //path to create the file to.
            var path = @"C:\\Users\Nolwazi Mlonzi\Desktop\" + ReportName + ".Xlsx";
            //create workbook and worksheet
            IWorkbook workbook = application.Workbooks.Create(1);
            IWorksheet worksheet = workbook.Worksheets[0];
            //populate data to the worksheet
            worksheet.ImportData(db.ProductItems.ToList(), 2, 1, false);
            //save workbook
            workbook.SaveAs(path, ",", HttpContext.ApplicationInstance.Response, ExcelDownloadType.Open, ExcelHttpContentType.Xlsx);
            workbook.Close();

            excelEngine.Dispose();

            return View();
        }

        public ActionResult CreateDocument()
        {
            //Create a new PDF document.
            PdfDocument doc = new PdfDocument();
            //Add a page.
            PdfPage page = doc.Pages.Add();
            //Create a PdfGrid.
            PdfGrid pdfGrid = new PdfGrid();
            //Create a DataTable.
            DataTable dataTable = new DataTable();
            //Add columns to the DataTable
            //dataTable.Columns.Add("ID");
            //dataTable.Columns.Add("Name");
            ////Add rows to the DataTable.
            //dataTable.Rows.Add(new object[] { "E01", "Clay" });
            //dataTable.Rows.Add(new object[] { "E02", "Thomas" });
            //dataTable.Rows.Add(new object[] { "E03", "Andrew" });
            //dataTable.Rows.Add(new object[] { "E04", "Paul" });
            //dataTable.Rows.Add(new object[] { "E05", "Gary" });
            //Assign data source.
            pdfGrid.DataSource = db.ProductItems.ToList();
            //Draw grid to the page of PDF document.
            pdfGrid.Draw(page, new PointF(10, 10));
            // Open the document in browser after saving it
            var ReportName = dateCreated.ToString("yyyy") + "-" + dateCreated.ToString("MM") + "-" + dateCreated.ToString("dd") + "-" + dateCreated.ToString("hh") + "" + dateCreated.ToString("mm") + " " + dateCreated.ToString("ss") + "pdf_report";
            doc.Save(ReportName + ".pdf", HttpContext.ApplicationInstance.Response, HttpReadType.Save);
            //close the document
            doc.Close(true);
            return View();
        }

        public ActionResult ReportView()
        {
            return View();
        }
    }
}